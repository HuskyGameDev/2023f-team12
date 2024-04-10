using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWall : MonoBehaviour
{



    [SerializeField] GameObject door;
    [SerializeField] GameObject button;
    private Animator doorAnimator;

    private AudioSource[] Sound;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();

        if (Util.TryGetComponent<Interactable>(button, out var inter))
        {
            inter.OnInteract += (_, _) =>
            {
                doorAnimator.Play("Base Layer.Wall");
            };
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
