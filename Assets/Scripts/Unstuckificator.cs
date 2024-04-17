using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unstuckificator : MonoBehaviour
{
    [SerializeField] bool enabled = false;
    private Vector3 SafetyPos;

    void Start()
    {
        SafetyPos = transform.Find("Safety").transform.position;
        Debug.Log(SafetyPos.x + "," + SafetyPos.y + "," + SafetyPos.z);
    }

    void Update()
    {
        if (enabled)
        {
            var myPos = transform.position;
            var playerPos = Global.Player.transform.position;
            if (Vector2.Distance(new Vector2(myPos.x, myPos.z), new Vector2(playerPos.x, playerPos.z)) < 1f)
            {
                Global.Player.transform.position = SafetyPos;
            }
        }
    }

    public void EnableTrigger()
    {
        enabled = true;
    }
}
