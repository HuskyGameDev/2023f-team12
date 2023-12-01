using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1R1Pinpad : Pinpad
{
    void Start()
    {
        OnSuccess += () =>
        {
            Debug.Log("WOOHOO");
        };
        OnFail += () =>
        {
            Debug.Log("GRAAAHHHHHH");
        };
    }
}
