using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBook : MonoBehaviour
{
    [SerializeField] GameObject hinge;

    void Start()
    {
        var item = GetComponent<Item>();
        var animator = hinge.GetComponent<Animator>();

        item.OnInspect += (_) =>
        {
            animator.Play("Base Layer.Book");
        };
        item.OnStopInspect += (_) =>
        {
            animator.Play("Base Layer.BookClose");
        };
    }
}
