using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uneggificator : MonoBehaviour
{
    void Start()
    {
        GetComponent<Interactable>().OnInteract += (_, _) => Global.Controller.Teleport(Global.Controller.initialPos);
    }
}
