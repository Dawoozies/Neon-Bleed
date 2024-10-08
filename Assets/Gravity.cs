using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gravity : PhysMovement, IVelocityComponent
{
    public Vector2 gravityDirection;
    public float gravityMax;
    public AnimationCurve airTimeGravityCurve;
    public float airTime;
    public float airTimeMax;
    public UnityEvent<float> onAirTime;

    public Vector2 v => gravity;
    public Vector2 gravity;

    protected override void CastCollisionCheck()
    {
        Vector2 projectedVel = gravityDirection;
        projectedVel.Scale(rb.velocity);
        Vector2 dv = (projectedVel + gravityDirection) * Time.fixedDeltaTime;
        results = Physics2D.CircleCastAll(rb.position, circleCollider.radius, dv.normalized, dv.magnitude, onCastCollisionLayerMask);
        if(results.Length > 0)
        {
            onCastCollision?.Invoke(rb.velocity, results);
        }
    }
    protected virtual void Update()
    {
        if (results.Length == 0)
        {
            airTime += Time.deltaTime;
        }
        else
        {
            airTime = 0f;
        }

        gravity = gravityMax * airTimeGravityCurve.Evaluate(airTime/airTimeMax) * gravityDirection;

        onAirTime?.Invoke(airTime);
    }
}
