using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    public Stats defaultStats;
    public Stats runtimeStats;
    void Awake()
    {
        runtimeStats.CopyStats(defaultStats);
    }
}
