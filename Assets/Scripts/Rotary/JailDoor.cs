using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject egg;
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
            else if (args.HeldObject?.name == "goldBar (61)")
            {
                Global.Controller.Teleport(egg.transform.position);
            }
        };
    }

}
