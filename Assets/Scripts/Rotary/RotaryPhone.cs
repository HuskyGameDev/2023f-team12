using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryPhone : MonoBehaviour
{
    [SerializeField] public string[] PhoneNumbers = new string[0];
    [SerializeField] public GameObject DoorToOpen = null;
    [SerializeField] public float DoorOpenTime = 5f;

    [SerializeField] GameObject TestObjectDeleteMePleaseItsOnTheTrashCan = null;

    internal string CorrectNumber;
    internal List<string> EnteredNumber = new();
    internal bool Correct = false;

    private float currTime = 0f;
    private Vector3 initialPos;

    void Start()
    {
        CorrectNumber = PhoneNumbers[Random.Range(0, PhoneNumbers.Length)];
        initialPos = DoorToOpen.transform.position;


        // TESTING ONLY
        TestObjectDeleteMePleaseItsOnTheTrashCan.GetComponent<Pinpad>().OnSuccess += () => EnteredNumber.Add("3");
    }

    void Update()
    {
        if (Correct)
        {
            if (currTime < DoorOpenTime)
            {
                currTime += Time.deltaTime;
                DoorToOpen.transform.position = Vector3.Lerp(initialPos, initialPos - new Vector3(5, 0, 0), currTime / DoorOpenTime);
            }
        }
        else
        {
            if (EnteredNumber.Count >= CorrectNumber.Length)
            {
                bool correct = EnteredNumber.Count == CorrectNumber.Length;
                for (int i = 0; i < CorrectNumber.Length; i++)
                {
                    if (CorrectNumber[i].ToString() != EnteredNumber[i])
                    {
                        correct = false;
                        break;
                    }
                }

                if (correct)
                {
                    Correct = true;
                }
                else
                {
                    EnteredNumber.Clear();
                }
            }
        }

    }
}
