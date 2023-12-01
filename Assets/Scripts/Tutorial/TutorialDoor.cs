using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoor : MonoBehaviour
{
    void Start()
    {
        GetComponent<Interactable>().OnInteract += (self, args) =>
        {
            if (args.HeldObject?.name == "Key_inprogress")
            {
                Global.LoadNewLevel(Level.One);
            }
        };
    }

    /*void Update()
    {
        
    }*/
}
