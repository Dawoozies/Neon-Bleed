using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodManager : MonoBehaviour
{
    public float blood;
    public float bleedIntensity;
    public float bleedParticleMax;
    public ParticleSystem bleedParticleSystem;
    public UnityEvent onBloodDepleted;
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
        emission.rateOverTime = bleedIntensity;
    }
    public void IncreaseBleedIntensity(float damage)
    {
        bleedIntensity += damage;
    }
}