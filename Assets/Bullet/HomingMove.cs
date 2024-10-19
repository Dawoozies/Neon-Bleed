using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;
public class HomingMove : PhysMovement, IVelocityComponent
{
    public float moveSpeed;
    public float detectMoveSpeed;
    public float detectTime;
    float detectTimer;
    public float detectRadius;
    public LayerMask detectLayers;

    Transform target;
    bool targetDetected;
    public UnityEvent onTargetFound;
    public UnityEvent onTargetNotFound;

    public Vector2 v => movement;
    Vector2 movement;
    public override void Update()
    {
        base.Update();
        if(!targetDetected)
        {
            FindTarget();
        }
        else
        {
            if(!LayerMaskUtility.IsInLayerMask(target.gameObject, detectLayers))
            {
                ClearTarget();
                return;
            }
            transform.right = (target.position - transform.position).normalized;
        }
    }
    public virtual void FindTarget()
    {
        if (detectTimer > 0)
        {
            detectTimer -= Time.deltaTime;
        }
        else
        {
            detectTimer = detectTime;
            Collider2D result = Physics2D.OverlapCircle(transform.position, detectRadius, detectLayers);
            if (result != null)
            {
                target = result.transform;
                targetDetected = true;
            }
        }
    }
    protected override void Move()
    {
        float finalMoveSpeed = moveSpeed;
        if (targetDetected)
        {
            finalMoveSpeed = detectMoveSpeed;
        }
        movement = transform.right * finalMoveSpeed;
    }
    public override void OnReturn()
    {
        base.OnReturn();
        ClearTarget();
    }
    public void ClearTarget()
    {
        target = null;
        targetDetected = false;
    }
    public override void SetVelocity(Vector3 v)
    {
        transform.right = v;
        moveSpeed = v.magnitude;
        movement = transform.right * moveSpeed;
    }
}