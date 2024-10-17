using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class BounceMove : PhysMovement
{
    public Vector2 initialVelocity;
    public float bounceFactor;
    public Vector2 randomSpeedBounds;
    protected override void Awake()
    {
        base.Awake();
        //rb.velocity = initialVelocity;
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
        rb.velocity = Vector2.Reflect(hitVelocity * bounceFactor, results[resultIndex].normal);
    }
    public void OnDisable()
    {
        initialVelocity = Random.insideUnitCircle * Random.Range(randomSpeedBounds.x, randomSpeedBounds.y);
    }
    public void OnEnable()
    {
        if(!Mathf.Approximately(randomSpeedBounds.sqrMagnitude, 0f))
        {
            initialVelocity = Random.insideUnitCircle * Random.Range(randomSpeedBounds.x, randomSpeedBounds.y);
        }
        rb.velocity = initialVelocity;
    }
}