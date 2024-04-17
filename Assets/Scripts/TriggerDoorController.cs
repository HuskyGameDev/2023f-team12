using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void onTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpen2", 0, 0.0f);
                
            }
            else if (closeTrigger)
            {
                myDoor.Play("DoorClose2", 0, 0.0f);
                
            }
        }
    }

}
