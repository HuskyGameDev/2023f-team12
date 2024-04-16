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
                animator.Play("Base Layer.Open");
                open = true;
            }
        };
    }
}
