using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailDoor : MonoBehaviour
{
    [SerializeField] GameObject door; 
    private Animator JailDoor;

    void Start()
    {
        JailDoor = door.GetComponent<Animator>();

        GetComponent<Interactable>().OnInteract += (self, args) =>
        {
            if (args.HeldObject?.name == "Key")
            {
                JailDoor.Play("Base Layer.JailDoor");
            }
        };
    }

}
