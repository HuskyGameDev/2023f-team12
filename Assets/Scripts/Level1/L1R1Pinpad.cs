using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1R1Pinpad : MonoBehaviour
{
    void Start()
    {
        Pinpad p = GetComponent<Pinpad>();
        
        p.OnKeyPress += (_, args) => { Debug.Log(args.Id); };
        p.OnSuccess += () => { Debug.Log("WOOHOO"); };
        p.OnFail += () => { Debug.Log("GRAAAHHHHHH"); };
    }
}
