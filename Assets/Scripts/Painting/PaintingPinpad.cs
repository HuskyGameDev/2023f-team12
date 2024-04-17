using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPinpad : MonoBehaviour
{

    [SerializeField] GameObject door;
    [SerializeField] GameObject safetyObj;
    [SerializeField] GameObject lightSuccessObj;
    [SerializeField] GameObject lightFailObj;
    [SerializeField] float lightStayOnTimer;

    private Animator doorAnimator;
    private Light lightSuccess;
    private Light lightFail;
    private Unstuckificator safety;

    private float currTimer;
    private bool lightOn;

    private AudioSource keyClick;
    private AudioSource successChime;

    void Start()
    {
        // Get stuff
        doorAnimator = door.GetComponent<Animator>();
        lightSuccess = lightSuccessObj.GetComponent<Light>();
        lightFail = lightFailObj.GetComponent<Light>();
        lightFail = lightFailObj.GetComponent<Light>();
        safety = safetyObj.GetComponent<Unstuckificator>();

        // Get sounds
        var audioSources = GetComponents<AudioSource>(); // populated in order in inspector
        keyClick = audioSources[0];
        successChime = audioSources[1];

        // Ensure lights are off by default
        lightSuccess.enabled = false;
        lightFail.enabled = false;

        // Pinpad and events
        Pinpad p = GetComponent<Pinpad>();
        
        p.OnSuccess += () => {
            lightSuccess.enabled = true;
            lightOn = true;
            doorAnimator.Play("Base Layer.OnKeypad");
            successChime.Play();
            StartCoroutine(EnableSafety());
        };
        p.OnFail += () =>
        {
            lightFail.enabled = true;
            lightOn = true;
        };
        p.OnKeyPress += (_, args) =>
        {
            keyClick.Play();
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

    IEnumerator EnableSafety()
    {
        yield return new WaitForSeconds(2);
        safety.EnableTrigger();
    }
}