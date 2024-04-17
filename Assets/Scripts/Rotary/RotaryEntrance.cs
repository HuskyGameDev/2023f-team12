using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryEntrance : MonoBehaviour
{
    [SerializeField] GameObject Handle;
    [SerializeField] GameObject door;

    public float delay = 3;
    float timer;
    bool open = false;

    private AudioSource[] doorCreak;
    private AudioSource[] doorClose;
    private Animator doorAnimator;

    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();

        var audioSources = GetComponents<AudioSource>();
        doorCreak = new AudioSource[] { audioSources[0], audioSources[1], audioSources[2], audioSources[3], audioSources[4] };
        doorClose = new AudioSource[] { audioSources[5], audioSources[6] };

        if (Util.TryGetComponent<Interactable>(Handle, out var inter))
        {
            inter.OnInteract += (_, _) =>
            {
                if (open) return;
                doorAnimator.Play("Base Layer.Open");
                doorCreak[Random.Range(0, doorCreak.Length)].Play();
                open = true;
            };
        }
    }


    void Update()
    { 
        if (open)
        {
            timer += Time.deltaTime;
            if(timer > delay)
            {
                Close();
                open = false;
            }
        }
        else
        {
            timer = 0;
        }
    }

    void Close()
    {
        doorAnimator = transform.parent.GetComponent<Animator>();
        doorAnimator.Play("Base Layer.Close");
        doorClose[Random.Range(0, doorClose.Length)].Play();
    }
}
