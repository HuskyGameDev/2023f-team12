using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryInteract()
    {
        OnInteractEventArgs args = new();
        args.HeldObject = Static.Player; // todo: get held object and stuff

        EventHandler<OnInteractEventArgs> handler = OnInteract;
        handler(this, args);
    }

    public event EventHandler<OnInteractEventArgs> OnInteract;

}

public class OnInteractEventArgs : EventArgs
{
    public GameObject HeldObject { get; set; }
}
