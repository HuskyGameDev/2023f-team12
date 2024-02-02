using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * HOW TO USE PINPAD:
 * - First, make sure your pinpad has both the Interactable and Pinpad components
 * - Set up the Pinpad component:
 *     - In the Keys property, add each key object, in order.
 *     - In the Combination property, add a combination, in order. Each number refers to the index of the key in the Keys property, not the actual pin.
 *     - Optionally, specify a submit key, reset code key and a screen to display the pin.
 *         - If submit key is not specified, the pin pad will automatically check the code when all digits are entered.
 * - Create a script for the object.
 *     - In the Start() method, get the Pinpad component.
 *     - Then, add your own methods to the OnKeyPress, OnSuccess, and OnFail events using +=
 *     - If you would like to reference an existing implementation, see L1R1Pinpad.cs
 */
public class Pinpad : MonoBehaviour
{
    private const float PROCESSING_TOTAL_TIME = 0.75f;

    public GameObject SubmitKey;
    public GameObject ResetKey;
    public GameObject[] Keys;
    public int[] Combination;
    public bool Enabled = true;

    private List<int> enteredCombo = new();
    private float processingTime = 0f;

    void Start()
    {
        // Set up event listeners on keys
        for(int i = 0; i < Keys.Length; i++) {
            // Get the key
            GameObject key = Keys[i];
            int j = i; // this is necessary for memory reasons
            
            // Make sure it's interactable, throw a fit if not
            Interactable interactable = key.GetComponent<Interactable>();
            if (interactable is null) throw new Exception("Keys must be interactable!");

            // Set an OnInteract event to add the combination id to the entered combo list, also activate OnKeyPress event
            interactable.OnInteract += (_, _) => {
                if (!Enabled || enteredCombo.Count == Combination.Length) return;

                enteredCombo.Add(j);

                OnKeyPressEventArgs args = new();
                args.Id = j;

                EventHandler<OnKeyPressEventArgs> handler = OnKeyPress;
                handler?.Invoke(this, args);
            };
        }

        // Set up submit functionality (it is optional, hence if-statement)
        if (SubmitKey != null)
        {
            Interactable interactable = SubmitKey.GetComponent<Interactable>();
            if (interactable is null) throw new Exception("Submit key must be interactable!");

            interactable.OnInteract += (_, _) => {
                CheckCombo();
            };
        }

        // Set up reset functionality (it is optional, hence if-statement)
        if (ResetKey != null)
        {
            Interactable interactable = ResetKey.GetComponent<Interactable>();
            if (interactable is null) throw new Exception("Reset key must be interactable!");

            interactable.OnInteract += (_, _) => {
                ResetCombo();
            };
        }
    }

    void Update()
    {
        // Check if pinpad is at max length
        if (SubmitKey == null && enteredCombo.Count == Combination.Length)
        {
            // Process it after a delay for suspense reasons
            processingTime += Time.deltaTime;
            if (processingTime >= PROCESSING_TOTAL_TIME)
            {
                CheckCombo();
            }
        }
    }

    void CheckCombo()
    {
        // Check if key was successful
        bool success = true;
        for (int i = 0; i < Combination.Length; i++)
        {
            if (Combination[i] != enteredCombo[i])
            {
                success = false;
                break;
            }
        }

        // Run event listeners
        if (success)
        {
            OnSuccess?.Invoke();
            Enabled = false;
        }
        else
        {
            OnFail?.Invoke();
        }

        // Reset vars
        processingTime = 0;
        enteredCombo.Clear();
    }

    void ResetCombo()
    {
        enteredCombo.Clear();
        processingTime = 0;
    }

    public event EventHandler<OnKeyPressEventArgs> OnKeyPress;
    public event Action OnSuccess;
    public event Action OnFail;

    public class OnKeyPressEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
}
