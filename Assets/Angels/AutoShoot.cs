using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AutoShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bulletIndex;
    public float shotCooldown;
    float cooldown;
    protected virtual void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = shotCooldown;
            Shoot();
        }
    }
    public virtual void Shoot()
    {
    }
}
