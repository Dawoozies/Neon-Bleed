using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;

public class BulletImpact : MonoBehaviour
{
    public float damage;
    public bool damagePlayer;
    public virtual void Impact(Vector3 hitVelocity, RaycastHit2D[] results)
    {
        if(results.Length > 0)
        {
            foreach (RaycastHit2D result in results)
            {
                if(damagePlayer)
                {
                    PlayerHealth playerHealth;
                    if(result.collider.gameObject.TryGetComponent(out playerHealth))
                    {
                        playerHealth.TakeDamage(damage);
                    }
                }
                else
                {
                    BloodManager bloodManager;
                    if (result.collider.gameObject.TryGetComponent(out bloodManager))
                    {
                        bloodManager.IncreaseBleedIntensity(damage);
                    }
                }
            }
        }
        SharedGameObjectPool.Return(gameObject);
    }
}
