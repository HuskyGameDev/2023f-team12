using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotaryPhone : MonoBehaviour
{
    [Header("Puzzle")]
    [SerializeField] public string[] PhoneNumbers = new string[0];

    [Header("Controls")]
    [SerializeField] public GameObject One;
    [SerializeField] public GameObject Two;
    [SerializeField] public GameObject Three;
    [SerializeField] public GameObject Four;
    [SerializeField] public GameObject Five;
    [SerializeField] public GameObject Six;
    [SerializeField] public GameObject Seven;
    [SerializeField] public GameObject Eight;
    [SerializeField] public GameObject Nine;
    [SerializeField] public GameObject Zero;
    [SerializeField] public GameObject Pound;
    [SerializeField] public GameObject Asterisk;

    [Header("Debug")]
    [SerializeField] public string CorrectNumber;
    internal List<string> EnteredNumber = new();
    internal bool Correct = false;
    internal bool Accepting = true;

    void Start()
    {
        if (PhoneNumbers.Length > 0)
        {
            CorrectNumber = PhoneNumbers[Random.Range(0, PhoneNumbers.Length)];
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                CorrectNumber += Random.Range(0, 10).ToString();
            }
        }

        // Deal with the controls and stuff
        if (Util.TryGetComponent<Interactable>(One,   out var inter1)) inter1.OnInteract += (_, _) => HandleButton("1");
        if (Util.TryGetComponent<Interactable>(Two,   out var inter2)) inter2.OnInteract += (_, _) => HandleButton("2");
        if (Util.TryGetComponent<Interactable>(Three, out var inter3)) inter3.OnInteract += (_, _) => HandleButton("3");
        if (Util.TryGetComponent<Interactable>(Four,  out var inter4)) inter4.OnInteract += (_, _) => HandleButton("4");
        if (Util.TryGetComponent<Interactable>(Five,  out var inter5)) inter5.OnInteract += (_, _) => HandleButton("5");
        if (Util.TryGetComponent<Interactable>(Six,   out var inter6)) inter6.OnInteract += (_, _) => HandleButton("6");
        if (Util.TryGetComponent<Interactable>(Seven, out var inter7)) inter7.OnInteract += (_, _) => HandleButton("7");
        if (Util.TryGetComponent<Interactable>(Eight, out var inter8)) inter8.OnInteract += (_, _) => HandleButton("8");
        if (Util.TryGetComponent<Interactable>(Nine,  out var inter9)) inter9.OnInteract += (_, _) => HandleButton("9");
        if (Util.TryGetComponent<Interactable>(Zero,  out var inter0)) inter0.OnInteract += (_, _) => HandleButton("0");
    }

    void Update()
    {
        Accepting = true; // TODO: REMOVE

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
                    Accepting = false;
                    OnSuccess?.Invoke();
                    Debug.Log("Phone number correct");
                }
                else
                {
                    EnteredNumber.Clear();
                    OnFail?.Invoke();
                    Debug.Log("Phone number incorrect");
                }
            }
        }
    }

    internal void HandleButton(string num)
    {
        Debug.Log(num);
        if (Accepting)
        {
            Accepting = false;
            EnteredNumber.Add(num);
        }
    }

    public event Action OnSuccess;
    public event Action OnFail;
}
