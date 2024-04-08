using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryEntrance : MonoBehaviour
{
    //[SerializeField] GameObject Handle;
    //[SerializeField] GameObject door;

    //public float delay = 3;
    //float timer;
    //bool open = false;
    //bool first = true;

    //[SerializeField] AudioSource DoorCreak;
    //[SerializeField] AudioSource DoorClose;
    //[SerializeField] Animator doorAnimator;

    //void Start()
    //{
    //    doorAnimator = door.GetComponent<Animator>();
    //    doorCreak = GetComponent<AudioSource>();

    //    var audioSources = GetComponents<AudioSource>();
    //    doorOpen = audioSources[0];
    //    DoorClose = audioSources[1];

    //    if (Util.TryGetComponent<Interactable>(Handle, out var inter))
    //    {
    //        inter.OnInteract += (_, _) =>
    //        {
    //            animator.Play("Base Layer.Door2Open");
    //            doorCreak.Play();
    //            open = true;
    //        };
    //    }
    //}


    //void Update()
    //{ 
    //    if (open)
    //    {
    //        timer += Time.deltaTime;
    //        if((timer > delay) && first) // first is included so the door only opens/closes once
    //        {
    //            Close();
    //            first = false;
    //            open = false;
    //        }
    //    }
    //}

    //void Close()
    //{
    //    doorAnimator = parent.GetComponent<Animator>();
    //    doorAnimator.Play("Base Layer.Door2Close");
    //    doorClose.Play();
    //}
}
