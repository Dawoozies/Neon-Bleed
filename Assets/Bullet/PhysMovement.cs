using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;
public class PhysMovement : MonoBehaviour, IPoolCallbackReceiver
{
    //on collision check
    //virtual method for movement
    //velocity at hit + list of cast results
    protected CircleCollider2D circleCollider; //making the decision to have everything be circles (at least their bounding boxes)
    protected Rigidbody2D rb;
    public LayerMask onCastCollisionLayerMask;
    public UnityEvent<Vector3, RaycastHit2D[]> onCastCollision;
    protected RaycastHit2D[] results;
    public float collisionCheckDelay;
    float collisionCheckDelayTimer;
    protected virtual void Move()
    {
        //code for moving here
        //for now we will just move in a specified direction
    }
    protected virtual void CastCollisionCheck()
    {
        Vector3 dv = rb.velocity * Time.fixedDeltaTime;
        results = Physics2D.CircleCastAll(rb.position, circleCollider.radius, dv.normalized, dv.magnitude, onCastCollisionLayerMask);
        if (results.Length > 0)
        {
            onCastCollision?.Invoke(rb.velocity, results);
        }
    }
    protected virtual void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void FixedUpdate()
    {
        if(collisionCheckDelayTimer <= 0)
        {
            CastCollisionCheck();
        }
        Move();
    }
    public virtual void SetVelocity(Vector3 v)
    {
        rb.velocity = v;
    }

    public virtual void OnRent()
    {
        collisionCheckDelayTimer = collisionCheckDelay;
    }
    public virtual void OnReturn()
    {
    }
    public virtual void Update()
    {
        if(collisionCheckDelayTimer > 0)
        {
            collisionCheckDelayTimer -= Time.deltaTime;
        }
    }
}