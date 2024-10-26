using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;

public class BloodManager : MonoBehaviour, IPoolCallbackReceiver
{
    public float bloodMax;
    public float blood;
    public float bleedIntensity;
    public float bleedIntensityMultiplier;
    public ParticleSystem bleedParticleSystem;
    public UnityEvent onBloodDepleted;
    List<Action<BloodManager>> onBloodDepletedCallbacks = new();
    protected virtual void Start()
    {
        blood = bloodMax;
    }
    protected virtual void Update()
    {
        var emission = bleedParticleSystem.emission;
        if (bleedIntensity > 0)
        {
            blood -= bleedIntensity*Time.deltaTime;
        }
        if(blood <= 0f)
        {
            blood = 0f;
            emission.rateOverTime = 0f;

            foreach (Action<BloodManager> callback in onBloodDepletedCallbacks)
            {
                callback.Invoke(this);
            }
            onBloodDepletedCallbacks.Clear();

            onBloodDepleted?.Invoke();



            return;
        }
        emission.rateOverTime = bleedIntensity * bleedIntensityMultiplier;
    }
    public virtual void IncreaseBleedIntensity(float damage)
    {
        bleedIntensity += damage;
    }
    public void RegisterOnBloodDepletedCallback(Action<BloodManager> a)
    {
        onBloodDepletedCallbacks.Add(a);
    }

    public void OnRent()
    {
    }

    public void OnReturn()
    {
        blood = bloodMax;
        bleedIntensity = 0f;
    }
}