using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailDoor : MonoBehaviour
{
    [SerializeField] GameObject door; 
    private Animator hinge;

    void Start()
    {
        hinge = door.GetComponent<Animator>();

        GetComponent<Interactable>().OnInteract += (self, args) =>
        {
            if (args.HeldObject?.name == "Key")
            {
                hinge.Play("Base Layer.Open");
            }
        };
    }

}
