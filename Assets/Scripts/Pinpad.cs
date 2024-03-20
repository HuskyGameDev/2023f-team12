using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * HOW TO USE PINPAD:
 * - The Pinpad component is intended for making events happen after a certain code sequence is entered by interacting with objects.
 *     - Its intended use is for a pinpad, but it can be used for similar code-entering things if necessary.
 * - Add Pinpad to the object (not the buttons, the object holding the buttons)
 * - Make all buttons you want to use part of the Interactable layer
 *     - The easiest way to do this is to set the layer of the holding object to Interactable and have Unity apply it recursively
 * - Drag the button children into the Keys array in numerical order, counting up from 0
 *     - The first object in the array will be the 0 button, the second object the 1 button, the third the 2 button and so on
 *     - If your button does not have number keys, pretend it does for the purposes of this
 * - Set up the combination in the combination array using the indices from the Keys array
 * - Optional: set up a submit key and/or reset key.
 *     - If a submit key is not given, the pinpad will automatically submit when the correct length is entered after a delay specified by ResponseDelay (in seconds).
 * - Create a custom script for your pinpad that references the Pinpad component.
 *     - This is how you add custom functionality to your pinpad, such as making it open a door on success or playing a sound on button press.
 *     - Use the OnKeyPress, OnSuccess, and OnFail events to add your own custom functionality. Use += for this.
 *     - For an example implementation, see PaintingPinpad.cs.
 */
public class Pinpad : MonoBehaviour
{
    [SerializeField] GameObject SubmitKey;
    [SerializeField] GameObject ResetKey;
    [SerializeField] GameObject[] Keys;
    [SerializeField] public int[] Combination;
    [SerializeField] public bool Enabled = true;
    [SerializeField] float ResponseDelay = 0.75f;

    public List<int> enteredCombo = new();
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
            if (processingTime >= ResponseDelay)
            {
                CheckCombo();
            }
        }
    }

    void CheckCombo()
    {
        // Check if key was successful
        bool success = true;

        if (Combination.Length == enteredCombo.Count)
        {
            // Combo is correct length
            for (int i = 0; i < Combination.Length; i++)
            {
                if (Combination[i] != enteredCombo[i])
                {
                    success = false;
                    break;
                }
            }
        }
        else
        {
            // Combo is not correct length
            success = false;
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
