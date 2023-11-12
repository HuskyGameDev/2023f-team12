using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public bool pickedUp;
    public bool inspecting;

    private const float MOVE_SPEED = 15f;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            float lookAngle = (Static.Controller.transform.rotation.eulerAngles.y - 60f) * -Mathf.Deg2Rad;
            float vertLookAngle = Static.Controller.cameraPitch * -Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(lookAngle) * Mathf.Cos(vertLookAngle / 2), Mathf.Sin(vertLookAngle) * 0.5f + 0.45f, Mathf.Sin(lookAngle) * Mathf.Cos(vertLookAngle / 2));
            if (inspecting)
            {
                transform.position = Vector3.Lerp(transform.position, Static.Player.transform.position + offset, MOVE_SPEED * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, Static.Player.transform.position + offset, MOVE_SPEED * Time.deltaTime);
            }
        }
    }

    public bool TryPickUp()
    {
        pickedUp = true;
        GetComponent<BoxCollider>().enabled = false;

        Action<Item> handler = OnPickUp;
        handler(this);
        return true;
    }

    public void TryInspect()
    {
        if (!pickedUp) return;
        inspecting = true;

        Action<Item> handler = OnInspect;
        handler(this);
    }

    public void StopInspect()
    {
        inspecting = false;
    }

    public void SetDown()
    {
        pickedUp = false;
    }

    public event Action<Item> OnPickUp;
    public event Action<Item> OnInspect;
}
