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

    public ObservedFloat ObservedPlayerBloodlust;
    public float bloodlustMaxCooldownReduction;
    public AnimationCurve bloodlustCurve;
    public float bloodlustMaxShotSpeedIncrease;

    [ReorderableList] public float[] angles;
    Vector2[] shotDirs;
    private void Start()
    {
        ProjectileCooldown.SetReference(cooldownTimeBase);
        RecalculateShotDirs();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            cooldownTimer = Mathf.Max(ProjectileCooldown.GetReference() - bloodlustMaxCooldownReduction* bloodlustCurve.Evaluate(ObservedPlayerBloodlust.GetReference()), minCooldown);
            ShootProjectile();
        }

        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    public virtual void ShootProjectile()
    {
        Vector2 aimDirRight = (MousePosition.ins.WorldPos - transform.position).normalized;
        Vector2 aimDirUp = Vector2.Perpendicular(aimDirRight);
        foreach (var shotDir in shotDirs)
        {
            Vector2 projectedDir = aimDirRight * shotDir.x + aimDirUp * shotDir.y;
            GameObject projectileClone = SharedGameObjectPool.Rent(prefab);
            projectileClone.transform.position = transform.position;
            projectileClone.GetComponentInChildren<PhysMovement>().SetVelocity(projectedDir * (shotSpeed + bloodlustMaxShotSpeedIncrease * bloodlustCurve.Evaluate(ObservedPlayerBloodlust.GetReference())));
        }

        //projectileClone.GetComponentInChildren<Rigidbody2D>().velocity = (MousePosition.ins.WorldPos - transform.position).normalized * shotSpeed;
    }
    public virtual void RecalculateShotDirs()
    {
        shotDirs = new Vector2[angles.Length];
        for (int i = 0; i < shotDirs.Length; i++)
        {
            shotDirs[i].x = Mathf.Cos(angles[i] * Mathf.Deg2Rad);
            shotDirs[i].y = Mathf.Sin(angles[i] * Mathf.Deg2Rad);
        }
    }
}