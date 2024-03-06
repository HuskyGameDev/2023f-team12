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
            Debug.Log("YIPPEE");
        };

        p.OnFail += () =>
        {
            Debug.Log("Boowomp");
        };


        p.OnKeyPress += (_, args) =>
        {
            Debug.Log(args.Id);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
