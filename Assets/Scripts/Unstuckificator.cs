using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unstuckificator : MonoBehaviour
{
    // [SerializeField] GameObject trigger;

    [SerializeField] bool enabled = false;
    private Vector2 SafetyPos;

    void Start()
    {
        // Trigger = transform.Find("Trigger").GetComponent<BoxCollider>();
        // GetComponent<BoxCollider>().enabled = false;
        SafetyPos = transform.Find("Safety").transform.position;
    }

    void Update()
    {
        if (enabled)
        {
            // https://docs.unity3d.com/ScriptReference/Physics.OverlapBox.html
            Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, 0);
            // Check when there is a new collider coming into contact with the box
            for (int i = 0; i < hitColliders.Length; i++)
            {
                var other = hitColliders[i];
                Debug.Log(other);
                if (enabled && other.tag == "Player")
                {
                    Global.Player.transform.position = SafetyPos;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
    }

    public void EnableTrigger()
    {
        enabled = true;
        // GetComponent<BoxCollider>().enabled = true;
    }
}
