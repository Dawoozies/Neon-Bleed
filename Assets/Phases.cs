using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;

public class Phases : MonoBehaviour, IPoolCallbackReceiver
{
    BossBloodManager bloodManager;
    [System.Serializable]
    public class Phase
    {
        public bool entered;
        public int bloodThreshold;
        public UnityEvent<int> onPhaseEnter;
    }
    float lastBloodValue;
    [ReorderableList] public Phase[] phases;
    private void Awake()
    {
        bloodManager = GetComponent<BossBloodManager>();
    }
    public void OnRent()
    {
        foreach (var phase in phases)
        {
            phase.entered = false;
        }
    }
    public void OnReturn()
    {
    }
    void Update()
    {
        if(lastBloodValue != bloodManager.blood)
        {
            for (int i = 0; i < phases.Length; i++)
            {
                Phase phase = phases[i];
                if (phase.entered)
                    continue;
                if(phase.bloodThreshold > bloodManager.blood)
                {
                    phase.entered = true;
                    phase.onPhaseEnter?.Invoke(i);
                }
            }
            lastBloodValue = bloodManager.blood;
        }
    }
}
