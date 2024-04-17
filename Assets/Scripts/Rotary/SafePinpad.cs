using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePinpad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Pinpad and events
        Pinpad p = GetComponent<Pinpad>();

        p.OnSuccess += () =>
        {
            GetComponent<Animator>().Play("Base Layer.OpenSmall");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
