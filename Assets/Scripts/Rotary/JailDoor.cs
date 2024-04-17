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
            Debug.Log(args.HeldObject?.name);
            if (args.HeldObject?.name == "Key")
            {
                hinge.Play("Base Layer.Open");
            }
        };
    }

}
