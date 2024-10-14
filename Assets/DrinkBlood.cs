using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using uPools;

public class DrinkBlood : MonoBehaviour
{
    public int bloodCollected;
    public float drinkRadius;
    public LayerMask angelBloodMask;
    public int particleEffectIndex;
    private void Update()
    {
        Collider2D[] toDrink = Physics2D.OverlapCircleAll(transform.position, drinkRadius, angelBloodMask);
        if(toDrink != null && toDrink.Length > 0)
        {
            foreach (Collider2D blood in toDrink)
            {
                bloodCollected++;
                SharedGameObjectPool.Return(blood.gameObject);
                ExternalEffects.ins.PlayEffect(particleEffectIndex, transform.position);
            }
        }
    }
}
