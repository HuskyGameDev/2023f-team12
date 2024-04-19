using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePinpad : MonoBehaviour
{
    private AudioSource pinClick;
    private AudioSource resetClick;
    private AudioSource openClick;

    void Start()
    {
        // Sound
        var audioSources = GetComponents<AudioSource>(); // populated as ordered in inspector
        pinClick = audioSources[0];
        resetClick = audioSources[1];
        openClick = audioSources[2];

        // Pinpad and events
        Pinpad p = GetComponent<Pinpad>();

        p.OnKeyPress += (_, _) =>
        {
            pinClick.Play();
        };
        p.OnReset += () => resetClick.Play();
        p.OnFail += () => resetClick.Play();
        p.OnSuccess += () =>
        {
            openClick.Play();
            GetComponent<Animator>().Play("Base Layer.OpenSmall");
        };
    }
}
