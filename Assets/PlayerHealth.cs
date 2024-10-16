using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public ObservedFloat ObservedPlayerMaxHealth;
    public ObservedFloat ObservedPlayerHealth;
    public ObservedInt ObservedPlayerBloodShield;
    public float defaultMaxHealthValue;
    public float defaultHealthValue;
    public int defaultShieldValue;
    public UnityEvent onPlayerDead;
    private void Start()
    {
        ObservedPlayerMaxHealth.SetReference(defaultMaxHealthValue);
        ObservedPlayerHealth.SetReference(defaultHealthValue);
        ObservedPlayerBloodShield.SetReference(defaultShieldValue);
    }
    public void TakeDamage(float damage)
    {
        int currentShieldValue = ObservedPlayerBloodShield.GetReference();
        if (currentShieldValue > 0)
        {
            ObservedPlayerBloodShield.SetReference(currentShieldValue - 1);
            return;
        }
        float newHealthValue = ObservedPlayerHealth.GetReference() - damage;
        ObservedPlayerHealth.SetReference(newHealthValue);
        if (newHealthValue <= 0)
        {
            onPlayerDead?.Invoke();
        }
    }
    public void RestoreAllShields()
    {
        float healthPerShield = (float)defaultMaxHealthValue / (float)defaultShieldValue;
        int shieldsRestored = 0;
        for (int i = 0; i < defaultShieldValue; i++)
        {
            if(ObservedPlayerHealth.GetReference() >= (float)i * healthPerShield)
            {
                //we can restore this shield
                shieldsRestored++;
            }
        }
        ObservedPlayerBloodShield.SetReference(shieldsRestored);
    }
}
