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
            if (slowDownTimeLeft > 0)
            {
                slowDownTimeLeft -= Time.unscaledDeltaTime;
                Time.timeScale = slowTime;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }


    }
    public void SlowDownTime(float slowDownTime)
    {
        slowDownTimeLeft = slowDownTime;
    }
}
