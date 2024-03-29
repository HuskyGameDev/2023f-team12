using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    void Start() { }

    void Update() { }

    public void Interact(Item heldItem)
    {
        OnInteractEventArgs args = new();
        args.HeldObject = heldItem?.gameObject;

        EventHandler<OnInteractEventArgs> handler = OnInteract;
        handler?.Invoke(this, args);
    }

    public event EventHandler<OnInteractEventArgs> OnInteract;


    public class OnInteractEventArgs : EventArgs
    {
        public GameObject HeldObject { get; set; }
    }
}
