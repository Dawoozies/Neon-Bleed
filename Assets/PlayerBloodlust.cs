using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBloodlust : MonoBehaviour
{
    public ObservedInt ObservedPlayerBloodCollected;
    public ObservedFloat ObservedPlayerHealth;
    public ObservedInt ObservedPlayerBloodShield;
    int OrderPriority = 0;
    public ObservedFloat ObservedPlayerBloodlust;
    public float bloodlustMaxValue;
    public float bloodlustValue;
    public AnimationCurve bloodlustCurve;
    public ObservedFloat BloodlustPercent;
    private void OnEnable()
    {
        ObservedPlayerBloodlust.SetReference(0);
        BloodlustPercent.SetReference(0);

        ObservedPlayerBloodCollected.RegisterObserver(OrderPriority, PlayerBloodCollected_OnSetReference);
        ObservedPlayerBloodShield.RegisterObserver(OrderPriority, PlayerBloodShield_OnSetReference);
        ObservedPlayerHealth.RegisterObserver(OrderPriority, PlayerHealth_OnSetReference);
        ObservedPlayerBloodlust.RegisterObserver(OrderPriority, PlayerBloodlust_OnSetReference);
    }
    private void OnDisable()
    {
        ObservedPlayerBloodCollected.UnregisterObserver(OrderPriority, PlayerBloodCollected_OnSetReference);
        ObservedPlayerBloodShield.UnregisterObserver(OrderPriority, PlayerBloodShield_OnSetReference);
        ObservedPlayerHealth.UnregisterObserver(OrderPriority, PlayerHealth_OnSetReference);
        ObservedPlayerBloodlust.UnregisterObserver(OrderPriority, PlayerBloodlust_OnSetReference);
    }
    void PlayerBloodCollected_OnSetReference(int previousValue, int newValue)
    {
        if(newValue > previousValue)
        {
            ObservedPlayerBloodlust.SetReference(ObservedPlayerBloodlust.GetReference() + 1);  
        }
    }
    void PlayerHealth_OnSetReference(float previousValue, float newValue)
    {
        if(newValue < previousValue)
        {
            //took health damage
            ObservedPlayerBloodlust.SetReference(0f);
        }
    }
    void PlayerBloodShield_OnSetReference(int previousValue, int newValue)
    {
        if (newValue < previousValue)
        {
            ObservedPlayerBloodlust.SetReference(
                ObservedPlayerBloodlust.GetReference() - (ObservedPlayerBloodlust.GetReference() / 4f)
                );
        }
    }
    void PlayerBloodlust_OnSetReference(float previousValue, float newValue)
    {
        BloodlustPercent.SetReference(bloodlustCurve.Evaluate(newValue / bloodlustMaxValue));
    }
}
