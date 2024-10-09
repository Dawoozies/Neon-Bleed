using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class MissileMove : PhysMovement, IPoolCallbackReceiver
{
    public Transform target;
    public SpriteRenderer missileGraphic;
    //spin this around when firing
    public float launchSpeed;
    public float missileAimAngularSpeed;
    public float missileAimTimeMax;
    float missileAimTime;
    public float randomFloatSpeedMax;
    public AnimationCurve aimCurve;
    Vector2 randomFloatDir;
    protected override void Start()
    {
        base.Start();
        onCastCollision.AddListener(OnCastCollisionHandler);
        randomFloatDir = Random.insideUnitCircle;
        missileAimTime = missileAimTimeMax;
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

    protected override void Move()
    {
        if(missileAimTime > 0)
        {
            missileAimTime -= Time.fixedDeltaTime;
            rb.angularVelocity = missileAimAngularSpeed * aimCurve.Evaluate(missileAimTime/missileAimTimeMax);
            rb.velocity = randomFloatDir * randomFloatSpeedMax * aimCurve.Evaluate(missileAimTime/missileAimTimeMax);
        }
        else
        {
            rb.angularVelocity = 0f;
            Vector3 dirToTarget = target.position - transform.position;
            transform.right = dirToTarget;
            rb.velocity = dirToTarget.normalized * launchSpeed;
        }
    }

    public void OnRent()
    {
        randomFloatDir = Random.insideUnitCircle;
        missileAimTime = missileAimTimeMax;
    }
    public void OnReturn()
    {
    }
}
