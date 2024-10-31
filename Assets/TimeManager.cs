using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager ins;
    private void Awake()
    {
        ins = this;
    }
    public KeyCode pauseKey;
    public bool pause;
    public float slowDownTimeLeft;
    public float slowTime;
    float requestSlowScale;
    float requestSlowTimeLeft;
    private void Update()
    {
        if(Input.GetKeyDown(pauseKey))
        {
            pause = !pause;
        }
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            bool timeSlowed = RequestedSlowUpdate() || SlowUpdate();
            if(!timeSlowed)
                Time.timeScale = 1f;
        }
    }
    public bool RequestedSlowUpdate()
    {
        if(requestSlowTimeLeft > 0)
        {
            requestSlowTimeLeft -= Time.unscaledDeltaTime;
            Time.timeScale = requestSlowScale;
            return true;
        }
        return false;
    }
    public bool SlowUpdate()
    {
        if (slowDownTimeLeft > 0)
        {
            slowDownTimeLeft -= Time.unscaledDeltaTime;
            Time.timeScale = slowTime;
            return true;
        }
        return false;
    }
    public void SlowDownTime(float slowDownTime)
    {
        slowDownTimeLeft = slowDownTime;
    }
    public void RequestSlowTime(float slowDownTime, float slowScale)
    {
        requestSlowTimeLeft = slowDownTime;
        requestSlowScale = slowScale;
    }
    public float InverseTimeScaleMultiplier()
    {
        if (Time.timeScale == 0)
            return 1f;

        return 1 / Time.timeScale;
    }
}
