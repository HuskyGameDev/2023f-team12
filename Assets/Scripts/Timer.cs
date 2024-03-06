using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float StartingTime;
    [SerializeField] bool StartActive;

    /// <summary>
    /// Time in seconds
    /// </summary>
    public float CurrentTime { get; protected set; }
    public bool Active { get; protected set; }

    private bool TriggeredOneMinuteWarn = false;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = StartingTime;
        Active = StartActive;

        if (CurrentTime < 60f)
        {
            TriggeredOneMinuteWarn = true; // don't trigger one minute warning if starting time is less than one minute
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            CurrentTime -= Time.deltaTime;
            
            if (CurrentTime < 60f && !TriggeredOneMinuteWarn)
            {
                OnOneMinuteLeft?.Invoke();
                TriggeredOneMinuteWarn = true;
            }

            if (CurrentTime <= 0f)
            {
                OnTimerFinish?.Invoke();
                Active = false;
            }
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

    public event Action OnTimerFinish;
    public event Action OnOneMinuteLeft;
    public event Action OnStart;
    public event Action OnStop;
}
