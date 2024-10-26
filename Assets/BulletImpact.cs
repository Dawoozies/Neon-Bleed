using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;

public class BulletImpact : DamageImpact
{
    public override void Impact(Vector3 hitVelocity, RaycastHit2D[] results)
    {
        base.Impact(hitVelocity, results);
        SharedGameObjectPool.Return(gameObject);
    }
}
