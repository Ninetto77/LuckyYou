using System;
using UnityEngine;

public class Timer: MonoBehaviour
{
    [SerializeField] private ProgressImageUI _progressUI;
    public event Action<bool> OnAlarm;
    public float TargetTime;
    public bool TimerOn = false;

    private float _targetTimeMax;
    
    
    public void SetTimer(float targetTime)
    {
        TargetTime = targetTime;
        _targetTimeMax = TargetTime;
        TimerOn = true;
    }

    public void TimerWorking()
    {
        if (TargetTime >= 0 && TimerOn) TargetTime -= Time.deltaTime;
        if (TargetTime <= 0 && TimerOn)
        {
            TimerOn = false;
            OnAlarm?.Invoke(true);
        }
    }

    public void TimerPause(bool value)
    {
        TimerOn = value;
    }

    public void TimerReset()
    {
        _progressUI.ProgressReset(0);
        TimerOn = false;
        TargetTime = 0;
    }

    private void Update()
    {
        TimerWorking();
        if (_progressUI != null) ProgressVisual();
    }

    private void ProgressVisual()
    {
        if (TimerOn) 
        {
            _progressUI.FillProgress(1 - TargetTime/ _targetTimeMax);
        }
    }
}
