using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public KeyCode pauseKey;
    public bool pause;
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
            Time.timeScale = 1f;
        }
    }
}
