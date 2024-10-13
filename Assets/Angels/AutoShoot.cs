using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AutoShoot : MonoBehaviour
{
    public int bulletIndex;
    public float shotCooldown;
    float cooldown;
    protected virtual void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if(cooldown <= 0)
        {
            cooldown = shotCooldown;
            Shoot();
        }
    }
    public virtual void Shoot()
    {
    }
}
