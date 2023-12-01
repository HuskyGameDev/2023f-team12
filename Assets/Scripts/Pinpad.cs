using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinpad : MonoBehaviour
{
    private const float PROCESSING_TOTAL_TIME = 0.75f;

    public GameObject Screen;
    public GameObject[] Keys;
    public int[] Combination;
    public bool Enabled = true;

    private List<int> enteredCombo = new();
    private float processingTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set up event listeners on keys
        for(int i = 0; i < Keys.Length; i++) {
            GameObject key = Keys[i];
            int j = i; // memory stuff
            Interactable interactable = key.GetComponent<Interactable>();

            if (interactable is null) throw new ArgumentException("Keys must be interactable!");

            interactable.OnInteract += (_, _) => {
                if (!Enabled || enteredCombo.Count == Combination.Length) return;

                enteredCombo.Add(j);
                Debug.Log(i);

                OnKeyPressEventArgs args = new();
                args.Id = j;

                EventHandler<OnKeyPressEventArgs> handler = OnKeyPress;
                handler?.Invoke(this, args);
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enteredCombo.Count == Combination.Length)
        {
            processingTime += Time.deltaTime;
            if (processingTime >= PROCESSING_TOTAL_TIME)
            {
                bool success = true;
                processingTime = 0;

                for (int i = 0; i < Combination.Length; i++)
                {
                    if (Combination[i] != enteredCombo[i])
                    {
                        success = false;
                        break;
                    }
                }

                if (success) OnSuccess?.Invoke();
                else OnFail?.Invoke();

                enteredCombo.Clear();
            }
        }
    }

    public event EventHandler<OnKeyPressEventArgs> OnKeyPress;
    public event Action OnSuccess;
    public event Action OnFail;

    public class OnKeyPressEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
}
