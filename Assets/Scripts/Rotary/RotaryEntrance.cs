using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryEntrance : MonoBehaviour
{
    [SerializeField] GameObject Handle;
    public float delay = 3;
    float timer;
    bool open = false;
    bool first = true;

    void Start()
    {
        GameObject parent = this.transform.parent.gameObject;
        Animator animator = parent.GetComponent<Animator>();
        AudioSource doorCreak = GetComponent<AudioSource>();
        if (Util.TryGetComponent<Interactable>(Handle, out var inter))
        {
            inter.OnInteract += (_, _) =>
            {
                animator.Play("Base Layer.Door2Open");
                doorCreak.Play();
                open = true;
            };
        }
    }


    void Update()
    { 
        if (open)
        {
            timer += Time.deltaTime;
            if((timer > delay) && first) // first is included so the door only opens/closes once
            {
                Close();
                first = false;
                open = false;
            }
        }
    }

    void Close()
    {
        Animator animator = parent.GetComponent<Animator>();
        animator.Play("Base Layer.Door2Close");
    }
}
