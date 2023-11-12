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
                Item asItem = conditionReference?.GetComponent<Item>();
                if (asItem is not null)
                {
                    asItem.OnPickUp += (_) => { conditionMet = true; };
                }
                break;
            case GateCondition.Inspect:
                break;
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        if (conditionMet)
        {
            if (!finishedMoving)
            {
                transform.Translate(new Vector3(endPos.x - startPos.x, endPos.y - startPos.y, endPos.z - startPos.z) * deltaTime / moveDuration);
                moveTime += deltaTime;
                finishedMoving = moveTime >= moveDuration;
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
