using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class BulletMove : PhysMovement
{
    protected override void Awake()
    {
        base.Awake();
        onCastCollision.AddListener(OnCastCollisionHandler);
    }
    protected virtual void OnCastCollisionHandler(Vector3 hitVelocity, RaycastHit2D[] results)
    {
        int resultIndex = 0;
        if (results[0].rigidbody == rb)
        {
            resultIndex = 1;

            if (results.Length == 1)
                return;
        }
        rb.position = results[resultIndex].centroid;
    }
}