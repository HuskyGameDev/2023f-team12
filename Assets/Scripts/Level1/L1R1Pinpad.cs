using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1R1Pinpad : MonoBehaviour
{

    [SerializeField] GameObject door;
    [SerializeField] GameObject lightSuccessObj;
    [SerializeField] GameObject lightFailObj;
    [SerializeField] float lightStayOnTimer;

    private Animator doorAnimator;
    private Light lightSuccess;
    private Light lightFail;

    private float currTimer;
    private bool lightOn;

    void Start()
    {
        // Get stuff
        doorAnimator = door.GetComponent<Animator>();
        lightSuccess = lightSuccessObj.GetComponent<Light>();
        lightFail = lightFailObj.GetComponent<Light>();

        // Ensure lights are off by default
        lightSuccess.enabled = false;
        lightFail.enabled = false;

        // Pinpad and events
        Pinpad p = GetComponent<Pinpad>();
        
        p.OnSuccess += () => {
            lightSuccess.enabled = true;
            lightOn = true;
            doorAnimator.Play("Base Layer.OnKeypad");
        };
        p.OnFail += () =>
        {
            lightFail.enabled = true;
            lightOn = true;
        };
        p.OnKeyPress += (_, _) =>
        {
            //lightFail.enabled = false;
        };
    }

    void Update()
    {
        if (lightOn)
        {
            currTimer += Time.deltaTime;
            if (currTimer > lightStayOnTimer)
            {
                lightSuccess.enabled = false;
                lightFail.enabled = false;
                lightOn = false;
                currTimer = 0f;
            }
        }
    }
}
