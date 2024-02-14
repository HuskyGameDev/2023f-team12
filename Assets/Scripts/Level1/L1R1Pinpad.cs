using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1R1Pinpad : MonoBehaviour
{

    [SerializeField] GameObject door;

    void Start()
    {
        Pinpad p = GetComponent<Pinpad>();
        
        p.OnSuccess += () => {
            Debug.Log("WOOHOO");
        };
    }
}
