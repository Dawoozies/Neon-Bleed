using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageToHitResults : MonoBehaviour
{
    public float damagePerSecond;
    public void DealDamage(RaycastHit2D[] results)
    {
        foreach (var result in results)
        {
            BloodManager bloodManager;
            if(result.collider.gameObject.TryGetComponent(out bloodManager))
            {
                bloodManager.IncreaseBleedIntensity(damagePerSecond*Time.deltaTime);
            }
        }
    }
}
