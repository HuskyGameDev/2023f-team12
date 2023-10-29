using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGate : MonoBehaviour
{
    public enum GateCondition
    {
        Move,
        PickUp,
        Inspect
    }

    [SerializeField] GateCondition conditionToOpen;
    [SerializeField] GameObject conditionReference;

    private bool conditionMet = false;
    private bool finishedMoving = false;

    void Start()
    {
        //
    }

    void Update()
    {
        if (conditionMet)
        {
            if (!finishedMoving)
            {
                finishedMoving = true;
                transform.position = new(transform.position.x, transform.position.y - 100, transform.position.z);
            }
        }
        else
        {
            switch (conditionToOpen)
            {
                case GateCondition.Move:
                    conditionMet = conditionReference.GetComponent<PlayerController>().currentDirVelocity != Vector2.zero;
                    break;
                case GateCondition.PickUp:
                    break;
                case GateCondition.Inspect:
                    break;
            }
        }
    }
}
