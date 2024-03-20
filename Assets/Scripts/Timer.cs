using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] bool StartActive;

    /// <summary>
    /// Time in seconds
    /// </summary>
    public float CurrentTime { get; protected set; }
    public bool Active { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = 0;
        Active = StartActive;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            CurrentTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        if (!Active)
        {
            OnStart?.Invoke();
        }
        Active = true;
    }

    public void StopTimer()
    {
        if (Active)
        {
            OnStop?.Invoke();
        }
        Active = false;
    }

    public event Action OnStart;
    public event Action OnStop;
}
