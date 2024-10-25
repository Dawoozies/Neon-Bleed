using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;

public class DrinkBlood : MonoBehaviour
{
    public ObservedInt PlayerMaxBloodRequired;
    public ObservedInt PlayerBloodCollected;
    public ObservedInt PlayerSoulPower;
    public int bloodRequiredForLevelUp;
    public int bloodRequiredIncreasePerLevel;
    public float drinkRadius;
    public LayerMask angelBloodMask;
    public int particleEffectIndex;
    public UnityEvent onPlayerLevelUp;
    private void Start()
    {
        PlayerMaxBloodRequired.SetReference(bloodRequiredForLevelUp);
        PlayerBloodCollected.SetReference(0);
        PlayerSoulPower.SetReference(0);
    }
    private void Update()
    {
        Collider2D[] toDrink = Physics2D.OverlapCircleAll(transform.position, drinkRadius, angelBloodMask);
        if(toDrink != null && toDrink.Length > 0)
        {
            foreach (Collider2D blood in toDrink)
            {
                PlayerBloodCollected.Increment();
                SharedGameObjectPool.Return(blood.gameObject);
                ExternalEffects.ins.PlayEffect(particleEffectIndex, transform.position);

                if (PlayerBloodCollected.GetReference() >= PlayerMaxBloodRequired.GetReference())
                {
                    onPlayerLevelUp?.Invoke();
                    PlayerBloodCollected.SetReference(0);
                    PlayerSoulPower.Increment();
                    PlayerMaxBloodRequired.SetReference(bloodRequiredForLevelUp + PlayerSoulPower.GetReference() * bloodRequiredIncreasePerLevel);
                }
            }
        }
    }
}
