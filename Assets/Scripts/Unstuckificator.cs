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
    }

    void LateUpdate()
    {
        if (enabled)
        {
            var myPos = transform.position;
            var playerPos = Global.Player.transform.position;
            var dist = Vector2.Distance(new Vector2(myPos.x, myPos.z), new Vector2(playerPos.x, playerPos.z));
            if (dist < 1f)
            {
                Global.Controller.Teleport(SafetyPos);
                enabled = false; // won't be stuck after hopefully
            }
            else if (dist > 5f)
            {
                enabled = false; // save useless calculations, they aren't stuck clearly
            }
        }
    }

    public void EnableTrigger()
    {
        enabled = true;
    }
}
