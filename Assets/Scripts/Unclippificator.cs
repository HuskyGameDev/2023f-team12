using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unclippificator : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<ItemClipDetector>();
        }
    }

    public class ItemClipDetector : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if (Util.TryGetComponent<Item>(collision.gameObject, out var item))
            {
                Debug.Log(item.gameObject.name + " clipped out of bounds!");
                item.ResetPosition();
            }
        }
    }
}
