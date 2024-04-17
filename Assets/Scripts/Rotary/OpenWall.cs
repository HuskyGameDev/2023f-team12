using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWall : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject safe;

    private bool pressed = false;

    void Start()
    {
        var doorAnimator = door.GetComponent<Animator>();

        GetComponent<Interactable>().OnInteract += (_, _) =>
        {
            if (!pressed)
            {
                pressed = true;
                doorAnimator.Play("Base Layer.Wall");
                StartCoroutine(SafeAnimation());
            }
        };
    }

    IEnumerator SafeAnimation()
    {
        yield return new WaitForSeconds(7);
        safe.GetComponent<Animator>().Play("Base Layer.OpenBig");
    }
}
