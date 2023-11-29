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
    [SerializeField] Vector3 endPos;
    [SerializeField] float moveDuration = 1f;

    private bool conditionMet = false;
    private bool finishedMoving = false;
    private float moveTime = 0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = gameObject.transform.position;
        
        switch (conditionToOpen)
        {
            case GateCondition.PickUp:
                {
                    Item item = conditionReference?.GetComponent<Item>();
                    if (item is not null)
                    {
                        item.OnPickUp += (_) => { conditionMet = true; };
                    }
                    break;
                }
            case GateCondition.Inspect:
                {
                    Item item = conditionReference?.GetComponent<Item>();
                    if (item is not null)
                    {
                        item.OnInspect += (_) => { conditionMet = true; };
                    }
                    break;
                }
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        if (conditionMet)
        {
            if (!finishedMoving)
            {
                transform.position = Vector3.Lerp(startPos, endPos, moveTime / moveDuration);
                moveTime += deltaTime;
                finishedMoving = moveTime >= moveDuration;
            }
        }
        else
        {
            if (conditionToOpen == GateCondition.Move)
            {
                conditionMet = conditionReference.GetComponent<PlayerController>().currentDirVelocity != Vector2.zero;
            }
        }
    }
}
