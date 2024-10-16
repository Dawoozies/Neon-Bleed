using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class MissileMove : PhysMovement
{
    public Transform target;
    public SpriteRenderer missileGraphic;
    //spin this around when firing
    public float missileAimAngularSpeed;
    public float missileAimTimeMax;
    float missileAimTime;
    public float randomFloatSpeedMax;
    public AnimationCurve aimCurve;
    Vector2 randomFloatDir;
    bool move;
    Vector3 velocity;
    public float upMoveSpeed;
    protected override void Start()
    {
        base.Start();
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

    protected override void Move()
    {
        if(missileAimTime > 0)
        {
            missileAimTime -= Time.fixedDeltaTime;
            rb.angularVelocity = missileAimAngularSpeed * aimCurve.Evaluate(missileAimTime/missileAimTimeMax);
            rb.velocity = (randomFloatDir * randomFloatSpeedMax + Vector2.up * upMoveSpeed) * aimCurve.Evaluate(missileAimTime/missileAimTimeMax);
        }
        else
        {
            rb.angularVelocity = 0f;
            rb.velocity = velocity;
            transform.right = rb.velocity;
        }
    }
    public override void SetVelocity(Vector3 v)
    {
        randomFloatDir = Random.insideUnitCircle;
        missileAimTime = missileAimTimeMax;
        velocity = v;
    }
}
