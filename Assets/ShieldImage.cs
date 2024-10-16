using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ShieldImage : MonoBehaviour, IObserver<int>
{
    public ObservedInt ObservedPlayerBloodShield;
    void OnEnable()
    {
        ObservedPlayerBloodShield.RegisterObserver(this);
    }
    void OnDisable()
    {
        ObservedPlayerBloodShield.UnregsiterObserver(this);
    }
    public int OrderPriority => 0;
    public int shieldValue;
    public UnityEvent<int> onShieldStateChange;
    public void OnSetReference(int previousRef, int newRef)
    {
        int shieldState = -1;
        if(previousRef >= shieldValue && newRef < shieldValue)
        {
            shieldState = 0;
        }
        if(previousRef < shieldValue && newRef >= shieldValue)
        {
            shieldState = 1;
        }
        onShieldStateChange?.Invoke(shieldState);
    }
}
