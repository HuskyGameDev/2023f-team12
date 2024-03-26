using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryEntrance : MonoBehaviour
{
    [SerializeField] GameObject Handle;

    void Start()
    {
        GameObject parent = this.transform.parent.parent.gameObject;
        Animator animator = parent.GetComponent<Animator>();
        AudioSource doorCreak = GetComponent<AudioSource>();
        if (Util.TryGetComponent<Interactable>(Handle, out var inter))
        {
            inter.OnInteract += (_, _) =>
            {
                animator.Play("Base Layer.OnKeypad");
                doorCreak.Play();
            };
        }
    }
}
