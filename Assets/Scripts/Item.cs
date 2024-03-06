using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public bool pickedUp;
    public bool inspecting;
    private Vector3 startPos;
    [SerializeField] public float holdOffset = 0.75f;
    [SerializeField] public float inspectOffset = 0.75f;

    private const float MOVE_SPEED = 15f;
    private const float ROT_SPEED = 15f;
    private const float ITEM_H_OFFSET = 0.75f;
    private const float ITEM_V_OFFSET_HOLD = 0.6f;
    private const float ITEM_V_OFFSET_INSP = 0.85f;
    private const float HOLD_ANGLE = 60f * Mathf.Deg2Rad;
    private const float INSP_ANGLE = 90f * Mathf.Deg2Rad;
    private const float FALL_SPEED_THRESH = 50f; // assuming gravity isn't changed elsewhere, should equate to falling for about 5 seconds

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (pickedUp)
        {
            // If you do not understand this math, don't worry. I don't either and I wrote the damn thing.
            float hLookAngle = (Global.Controller.transform.rotation.eulerAngles.y) * -Mathf.Deg2Rad;
            float vLookAngle = Global.Controller.cameraPitch * -Mathf.Deg2Rad;
            Vector3 offset;
            if (inspecting)
            {
                offset = new Vector3(Mathf.Cos(hLookAngle + INSP_ANGLE) * Mathf.Cos(vLookAngle) * inspectOffset, Mathf.Sin(vLookAngle) * 0.65f + ITEM_V_OFFSET_INSP, Mathf.Sin(hLookAngle + INSP_ANGLE) * Mathf.Cos(vLookAngle) * inspectOffset);
                transform.position = Vector3.Lerp(transform.position, Global.Player.transform.position + offset, MOVE_SPEED * Time.deltaTime);
            }
            else
            {
                offset = new Vector3(Mathf.Cos(hLookAngle + HOLD_ANGLE) * Mathf.Cos(vLookAngle * 0.5f) * holdOffset, Mathf.Sin(vLookAngle) * 0.625f + ITEM_V_OFFSET_HOLD, Mathf.Sin(hLookAngle + HOLD_ANGLE) * Mathf.Cos(vLookAngle * 0.5f) * holdOffset);
                transform.position = Vector3.Lerp(transform.position, Global.Player.transform.position + offset, MOVE_SPEED * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Global.Player.transform.position - new Vector3(transform.position.x, Global.Player.transform.position.y, transform.position.z)), ROT_SPEED * Time.deltaTime);
            }
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (Mathf.Abs(rb.velocity.y) > FALL_SPEED_THRESH)
        {
            transform.position = startPos;
            rb.velocity = Vector3.zero;
        }
    }

    public void PickUp()
    {
        pickedUp = true;
        //GetComponent<BoxCollider>().enabled = false;
        GetComponent<BoxCollider>().excludeLayers = GetComponent<BoxCollider>().excludeLayers.value | Util.LayerMask(2);
        GetComponent<Rigidbody>().useGravity = false;

        Action<Item> handler = OnPickUp;
        handler?.Invoke(this);
    }

    public void SetDown()
    {
        pickedUp = false;
        //GetComponent<BoxCollider>().enabled = true;
        GetComponent<BoxCollider>().excludeLayers = GetComponent<BoxCollider>().excludeLayers.value & ~Util.LayerMask(2);
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void Inspect()
    {
        if (!pickedUp) return;
        inspecting = true;

        Action<Item> handler = OnInspect;
        handler?.Invoke(this);
    }
    public void StopInspecting()
    {
        if (!pickedUp) return;
        inspecting = false;
    }

    public event Action<Item> OnPickUp;
    public event Action<Item> OnInspect;
}
