using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryPickUp()
    {
        Action<Item> handler = OnPickUp;
        handler(this);
        return true;
    }

    public event Action<Item> OnPickUp;
}
