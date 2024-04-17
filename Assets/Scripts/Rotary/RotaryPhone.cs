using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RotaryPhone : MonoBehaviour
{
    private const float RotToSpeed = 270f;
    private const float RotFromSpeed = RotToSpeed * -0.6f;

    [Header("Puzzle")]
    [SerializeField] public string[] PhoneNumbers = new string[0];

    [Header("Controls")]
    [SerializeField] public GameObject Plate;
    [Space(12)]
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
    [SerializeField] internal bool Correct = false;
    internal bool Rotating = false;

    private float rotToAngle;
    private float origAngle;

    void Start()
    {
        origAngle = Plate.transform.localEulerAngles.z;
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
        // 74.818
        if (Util.TryGetComponent<Interactable>(One,      out var inter1)) inter1.OnInteract += (_, _) => HandleButton("1",  80.662f - 74.818f);
        if (Util.TryGetComponent<Interactable>(Two,      out var inter2)) inter2.OnInteract += (_, _) => HandleButton("2", 107.91f  - 74.818f);
        if (Util.TryGetComponent<Interactable>(Three,    out var inter3)) inter3.OnInteract += (_, _) => HandleButton("3", 134.395f - 74.818f);
        if (Util.TryGetComponent<Interactable>(Four,     out var inter4)) inter4.OnInteract += (_, _) => HandleButton("4", 163.084f - 74.818f);
        if (Util.TryGetComponent<Interactable>(Five,     out var inter5)) inter5.OnInteract += (_, _) => HandleButton("5", 190.532f - 74.818f);
        if (Util.TryGetComponent<Interactable>(Six,      out var inter6)) inter6.OnInteract += (_, _) => HandleButton("6", 218.57f  - 74.818f);
        if (Util.TryGetComponent<Interactable>(Seven,    out var inter7)) inter7.OnInteract += (_, _) => HandleButton("7", 245.1f   - 74.818f);
        if (Util.TryGetComponent<Interactable>(Eight,    out var inter8)) inter8.OnInteract += (_, _) => HandleButton("8", 273.482f - 74.818f);
        if (Util.TryGetComponent<Interactable>(Nine,     out var inter9)) inter9.OnInteract += (_, _) => HandleButton("9", 299.201f - 74.818f);
        if (Util.TryGetComponent<Interactable>(Zero,     out var inter0)) inter0.OnInteract += (_, _) => HandleButton("0", 326.8f   - 74.818f);
        //if (Util.TryGetComponent<Interactable>(Pound,    out var interP)) interP.OnInteract += (_, _) => HandleButton("#", 378.703f);
        //if (Util.TryGetComponent<Interactable>(Asterisk, out var interA)) interA.OnInteract += (_, _) => HandleButton("*", 353.904f);
    }

    void Update()
    {
        if (Correct)
        {
            // do stuff

        }
        else if (Rotating)
        {
            bool rotTo = rotToAngle != origAngle;
            float oldRot = Plate.transform.localEulerAngles.z;
            float delta = Time.deltaTime * (rotTo ? RotToSpeed : RotFromSpeed);
            float newRot = oldRot + delta;

            bool end = false;
            if (Mathf.Sign(delta) > 0 && oldRot < rotToAngle && newRot >= rotToAngle)
            {
                newRot -= newRot - rotToAngle;
                end = true;
            }
            else if (Mathf.Sign(delta) < 0 && oldRot > rotToAngle && newRot <= rotToAngle)
            {
                newRot += newRot - rotToAngle;
                end = true;
            }

            if (end)
            {
                if (rotToAngle == origAngle)
                {
                    Rotating = false;
                }
                else
                {
                    rotToAngle = origAngle;
                }
            }

            var fullRot = Plate.transform.localEulerAngles;
            Plate.transform.localEulerAngles = new Vector3(fullRot.x, fullRot.y, newRot);
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
                    OnSuccess?.Invoke();
                    SceneManager.LoadScene(3);
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

    internal void HandleButton(string num, float rot)
    {
        Debug.Log(num);
        if (!Correct && !Rotating)
        {
            Rotating = true;
            rotToAngle = (origAngle + rot) % 360f;
            EnteredNumber.Add(num);
        }
    }

    public event Action OnSuccess;
    public event Action OnFail;
}
