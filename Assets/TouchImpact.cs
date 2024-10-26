using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;
public class TouchImpact : DamageImpact
{
    public float damageCooldownTime;
    float damageCooldownTimer;
    public override void Impact(Vector3 hitVelocity, RaycastHit2D[] results)
    {
        if (damageCooldownTimer > 0)
            return;

        if (results.Length > 0)
        {
            foreach (var result in results)
            {
                if (damagePlayer)
                {
                    PlayerHealth playerHealth;
                    if (result.collider.gameObject.TryGetComponent(out playerHealth))
                    {
                        playerHealth.TakeDamage(damage);
                        damageCooldownTimer = damageCooldownTime;
                    }
                }
                else
                {
                    BloodManager bloodManager;
                    if (result.collider.gameObject.TryGetComponent(out bloodManager))
                    {
                        bloodManager.IncreaseBleedIntensity(damage);
                        damageCooldownTimer = damageCooldownTime;
                    }
                }
            }
        }

    }
    public void Update()
    {
        if (damageCooldownTimer > 0)
            damageCooldownTimer -= Time.deltaTime;
    }
}
