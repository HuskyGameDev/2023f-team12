using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float MOUSE_MOVE_THRESH = 10f; // pixels
    [SerializeField] public Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float smoothMoveTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    [SerializeField] float reach = 4f;
    [SerializeField] float inspectReach = 1f;

    public float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    public CharacterController controller = null;
    private Vector3 beginClick;

    public Vector2 currentDir = Vector2.zero;
    public Vector2 currentDirVelocity = Vector2.zero;

    private GameObject crosshair;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    void Start()
    {
        Global.Player = gameObject;
        Global.Controller = this;
        crosshair = GameObject.Find("Crosshair");

        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    void Update()
    {
        // Deal with movement
        UpdateMouseLook();
        UpdateMovement();

        // Deal with interacting/picking up objects
        if (Input.GetMouseButtonDown(0))
        {
            beginClick = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnClick(false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            OnClick(true);
        }

        // Switching things in the inventory (only do when not inspecting)
        if (!Inspecting)
        {
            if (Input.mouseScrollDelta.y > 0f)
            {
                Global.NextItem();
            }
            else if (Input.mouseScrollDelta.y < 0f)
            {
                Global.PrevItem();
            }
        }

        // Temp solution until pause menu: escape quits game
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        
        if (Inspecting)
        {
            // While inspecting, rotate the held object if mouse is held
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (Input.GetMouseButton(0))
            {
                float lookAngle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
                HeldItem.transform.RotateAround(HeldItem.transform.position, Vector3.up, -currentMouseDelta.x * mouseSensitivity);
                HeldItem.transform.RotateAround(HeldItem.transform.position, (Vector3.right * -Mathf.Cos(lookAngle) + Vector3.forward * Mathf.Sin(lookAngle)).normalized, -currentMouseDelta.y * mouseSensitivity);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // While not inspecting, rotate the camera
            cameraPitch -= currentMouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

            playerCamera.localEulerAngles = Vector3.right * cameraPitch;
            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }
    }

    void UpdateMovement()
    {
        // Movement set to 0 when inspecting so we don't move while looking at object
        Vector2 targetDir = Inspecting ? Vector2.zero : new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, smoothMoveTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnClick(bool rightClick)
    {
        if (rightClick)
        {
            // Right click toggles inspecting
            Inspecting = !Inspecting;
            crosshair?.SetActive(!Inspecting);
        }
        else if (Inspecting)
        {
            // We are inspecting, interact with the object in our hand
            Camera cam = null;
            if (Vector3.Distance(beginClick, Input.mousePosition) < MOUSE_MOVE_THRESH && Util.TryGetComponent<Camera>(playerCamera.gameObject, out cam))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit info;
                bool result = Physics.Raycast(ray, out info);

                if (result && info.distance < inspectReach && info.collider.gameObject.layer == 7)
                {
                    Debug.Log("AAAAA");
                }
                else
                {
                    Debug.Log("BBBBBB");
                }
            }
            else if (cam == null)
            {
                Debug.Log("CCCCC");
            }
        }
        else
        {
            // We are not inspecting, therefore we can look for objects to pick up.

            // Create raycast
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);
            RaycastHit info;
            bool result = Physics.Raycast(ray, out info);

            // Make sure we hit something, it's within reach, and it is part of the interactable layer.
            if (result && info.distance < reach && info.collider.gameObject.layer == 6)
            {

                GameObject obj = info.collider.gameObject;

                // Do stuff with the object
                if (!HoldingItem && Util.TryGetComponent<Item>(obj, out Item item))
                {
                    item.PickUp();
                    HeldItem = item;
                }
                else if (Util.TryGetComponent<Interactable>(obj, out Interactable interactable))
                {
                    interactable.Interact(HeldItem);
                }
            }
            else if (HoldingItem)
            {
                // Set down item
                HeldItem.SetDown();
                HeldItem = null;
            }
        }
    }

    private Item HeldItem
    {
        get
        {
            return Global.Inventory[Global.HeldItem];
        }
        set
        {
            Global.Inventory[Global.HeldItem] = value;
        }
    }

    private bool HoldingItem
    {
        get
        {
            return HeldItem is not null;
        }
    }
    
    private bool Inspecting
    {
        get
        {
            return HoldingItem && HeldItem.inspecting;
        }
        set
        {
            if (HoldingItem)
            {
                if(value)
                {
                    HeldItem.Inspect();
                }
                else
                {
                    HeldItem.StopInspecting();
                }
            }
        }
    }
}
