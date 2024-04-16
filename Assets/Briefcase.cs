using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Briefcase : MonoBehaviour
{
    [SerializeField] GameObject hinge;
    private Animator animator;
    private bool open = false;

    void Start()
    {
        animator = hinge.GetComponent<Animator>();

        GetComponent<Interactable>().OnInteract += (self, args) =>
        {
            if (!open)
            {
                open = true;
                animator.Play("Base Layer.BriefcaseOpen");
            }
        };
    }
}
