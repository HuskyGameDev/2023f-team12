using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{

    public static event Action DoorClose;
    [SerializeField] GameObject player;

    // Update is called once per frame
    void Update()
    {
        if ()
        {
            DoorClose?.Invoke();
        }
    }
}
