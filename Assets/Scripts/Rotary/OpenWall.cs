using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWall : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject safe;
    [SerializeField] GameObject safetyObj;

    private bool pressed = false;
    private Unstuckificator safety;

    void Start()
    {
        var doorAnimator = door.GetComponent<Animator>();
        safety = safetyObj.GetComponent<Unstuckificator>();

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
        StartCoroutine(EnableSafety());
    }

    IEnumerator EnableSafety()
    {
        yield return new WaitForSeconds(5);
        safety.EnableTrigger();
    }
}