using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryPhone : MonoBehaviour
{
    [SerializeField] public string[] PhoneNumbers = new string[0];

    internal string CorrectNumber;
    internal List<string> EnteredNumber = new();
    internal bool Correct = false;

    void Start()
    {
        CorrectNumber = PhoneNumbers[Random.Range(0, PhoneNumbers.Length)];
    }

    void Update()
    {
        if (Correct)
        {
            // do stuff
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
