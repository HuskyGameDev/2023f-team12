using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public bool pickedUp;
    public bool inspecting;

    private const float MOVE_SPEED = 15f;
    private const float ROT_SPEED = 15f;
    private const float ITEM_H_OFFSET = 0.75f;
    private const float ITEM_V_OFFSET = 0.4f;
    private const float HOLD_ANGLE = 60f * Mathf.Deg2Rad;
    private const float INSP_ANGLE = 90f * Mathf.Deg2Rad;

    void Start()
    {
        //
    }

    void Update()
    {
        if (pickedUp)
        {
            float hLookAngle = (Static.Controller.transform.rotation.eulerAngles.y) * -Mathf.Deg2Rad;
            float vLookAngle = Static.Controller.cameraPitch * -Mathf.Deg2Rad;
            Vector3 offset;
            if (inspecting)
            {
                offset = new Vector3(Mathf.Cos(hLookAngle + INSP_ANGLE) * Mathf.Cos(vLookAngle) * ITEM_H_OFFSET, Mathf.Sin(vLookAngle) * 0.5f + 0.45f, Mathf.Sin(hLookAngle + INSP_ANGLE) * Mathf.Cos(vLookAngle) * ITEM_H_OFFSET);
                transform.position = Vector3.Lerp(transform.position, Static.Player.transform.position + offset, MOVE_SPEED * Time.deltaTime);
            }
            else
            {
                offset = new Vector3(Mathf.Cos(hLookAngle + HOLD_ANGLE) * Mathf.Cos(vLookAngle * 0.5f) * ITEM_H_OFFSET, Mathf.Sin(vLookAngle) * 0.625f + ITEM_V_OFFSET, Mathf.Sin(hLookAngle + HOLD_ANGLE) * Mathf.Cos(vLookAngle * 0.5f) * ITEM_H_OFFSET);
                transform.position = Vector3.Lerp(transform.position, Static.Player.transform.position + offset, MOVE_SPEED * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Static.Player.transform.position - new Vector3(transform.position.x, Static.Player.transform.position.y, transform.position.z)), ROT_SPEED * Time.deltaTime);
            }
        }
    }

    public void PickUp()
    {
        pickedUp = true;
        GetComponent<BoxCollider>().enabled = false;

        Action<Item> handler = OnPickUp;
        handler(this);
    }

    public void SetDown()
    {
        pickedUp = false;
        GetComponent<BoxCollider>().enabled = true;
    }

    public void Inspect()
    {
        if (!pickedUp) return;
        inspecting = true;

        Action<Item> handler = OnInspect;
        handler(this);
    }
    public void StopInspecting()
    {
        if (!pickedUp) return;
        inspecting = false;
    }

    public event Action<Item> OnPickUp;
    public event Action<Item> OnInspect;
}
