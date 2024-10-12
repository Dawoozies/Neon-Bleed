using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;

public class BloodManager : MonoBehaviour, IPoolCallbackReceiver
{
    public float bloodMax;
    float blood;
    public float bleedIntensity;
    public float bleedIntensityMultiplier;
    public ParticleSystem bleedParticleSystem;
    public UnityEvent onBloodDepleted;
    void Start()
    {
        blood = bloodMax;
    }
    private void Update()
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

            onBloodDepleted?.Invoke();

            return;
        }
        emission.rateOverTime = bleedIntensity * bleedIntensityMultiplier;
    }
    public void IncreaseBleedIntensity(float damage)
    {
        bleedIntensity += damage;
    }

    public void OnRent()
    {
        blood = bloodMax;
    }
    public void OnReturn()
    {
    }
}