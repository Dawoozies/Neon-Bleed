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
    private void OnEnable()
    {
        ObservedPlayerBloodCollected.RegisterObserver(OrderPriority, PlayerBloodCollected_OnSetReference);
        ObservedPlayerBloodShield.RegisterObserver(OrderPriority, PlayerBloodShield_OnSetReference);
        ObservedPlayerHealth.RegisterObserver(OrderPriority, PlayerHealth_OnSetReference);
        ObservedPlayerBloodlust.SetReference(0);
    }
    private void OnDisable()
    {
        ObservedPlayerBloodCollected.UnregisterObserver(OrderPriority, PlayerBloodCollected_OnSetReference);
        ObservedPlayerBloodShield.UnregisterObserver(OrderPriority, PlayerBloodShield_OnSetReference);
        ObservedPlayerHealth.UnregisterObserver(OrderPriority, PlayerHealth_OnSetReference);
    }
    void PlayerBloodCollected_OnSetReference(int previousValue, int newValue)
    {
        if(newValue > previousValue)
        {
            bloodlustValue++;
            SetBloodlustReference();
        }
    }
    void PlayerHealth_OnSetReference(float previousValue, float newValue)
    {
        if(newValue < previousValue)
        {
            //took health damage
            bloodlustValue = 0f;
            SetBloodlustReference();
        }
    }
    void PlayerBloodShield_OnSetReference(int previousValue, int newValue)
    {
        if (newValue < previousValue)
        {
            bloodlustValue = bloodlustValue - (bloodlustValue / 4f);
            SetBloodlustReference();
        }
    }
    void SetBloodlustReference()
    {
        ObservedPlayerBloodlust.SetReference(bloodlustCurve.Evaluate(bloodlustValue / bloodlustMaxValue));
    }
}
