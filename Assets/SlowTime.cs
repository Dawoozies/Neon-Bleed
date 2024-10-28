using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    public float slowTime;
    public void Slow()
    {
        TimeManager.ins.SlowDownTime(slowTime);
    }
}
