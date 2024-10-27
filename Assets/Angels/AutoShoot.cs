using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AutoShoot : MonoBehaviour
{
    public int bulletIndex;
    public float shotCooldown;
    float cooldown;
    float manualCooldown;
    public bool manualOnly;
    protected virtual void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        if(manualCooldown > 0)
        {
            manualCooldown -= Time.deltaTime;
        }

        if(cooldown <= 0)
        {
            cooldown = shotCooldown;
            if(!manualOnly)
                Shoot();
        }
    }
    public virtual void Shoot()
    {
    }
    public virtual void ManuallyShoot()
    {
        if (manualCooldown > 0)
            return;
        Shoot();
        manualCooldown = shotCooldown;
    }
}