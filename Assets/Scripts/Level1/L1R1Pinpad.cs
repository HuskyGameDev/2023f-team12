using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1R1Pinpad : MonoBehaviour
{

    [SerializeField] GameObject door;
    [SerializeField] GameObject lightSuccessObj;
    [SerializeField] GameObject lightFailObj;

    private Animator doorAnimator;
    private Light lightSuccess;
    private Light lightFail;

    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
        lightSuccess = lightSuccessObj.GetComponent<Light>();
        lightFail = lightFailObj.GetComponent<Light>();

        lightSuccess.enabled = false;
        lightFail.enabled = false;

        Pinpad p = GetComponent<Pinpad>();
        
        p.OnSuccess += () => {
            lightSuccess.enabled = true;
            Debug.Log("Correct :)");
        };
        p.OnFail += () =>
        {
            lightFail.enabled = true;
            Debug.Log("Incorrect :(");
        };
        p.OnKeyPress += (_, _) =>
        {
            lightFail.enabled = false;
            Debug.Log("Tap!");
        };
    }
}
