using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;
public class Projectile : MonoBehaviour
{
    public GameObject prefab;
    public float cooldownTime;
    float cooldownTimer;
    public float shotSpeed;
    private void Start()
    {
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            cooldownTimer = cooldownTime;
            ShootProjectile();
        }

        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    public virtual void ShootProjectile()
    {
        GameObject projectileClone = SharedGameObjectPool.Rent(prefab);
        projectileClone.transform.position = transform.position;
        projectileClone.GetComponentInChildren<PhysMovement>().SetVelocity((MousePosition.ins.WorldPos - transform.position).normalized * shotSpeed);
        //projectileClone.GetComponentInChildren<Rigidbody2D>().velocity = (MousePosition.ins.WorldPos - transform.position).normalized * shotSpeed;
    }
}