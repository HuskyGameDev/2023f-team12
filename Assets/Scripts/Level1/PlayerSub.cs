using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSub : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.DoorClose += ChangeLocation;
    }

    // Update is called once per frame
    public void ChangeLocation()
    {
        //Play animation of player being drapped back upstairs
    }

    private void OnTriggerEnter3D(Collider3D collision)
    {
        EventManger.StartDoorEvent();
    }
}
