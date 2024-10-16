using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;
public class Projectile : MonoBehaviour
{
    public ObservedFloat ProjectileCooldown;
    public GameObject prefab;
    public float minCooldown;
    public float cooldownTimeBase;
    float cooldownTimer;
    public float shotSpeed;
    private void Start()
    {
        ProjectileCooldown.SetReference(cooldownTimeBase);
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            cooldownTimer = ProjectileCooldown.GetReference();
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