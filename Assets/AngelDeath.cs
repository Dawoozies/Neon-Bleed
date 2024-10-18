using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class AngelDeath : MonoBehaviour, IPoolCallbackReceiver
{
    bool dead;
    Rigidbody2D rb;
    float originalMass;
    public float deathMass;
    float originalGravity;
    public float deathGravity;
    LayerMask originalExclusionLayers;
    public LayerMask deathExclusionLayer;
    public float deathVelocity;
    public Vector2 angularSpinOnDeathBounds;
    public float yValueDestroy;
    [Layer] public int deathLayer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalMass = rb.mass;
        originalGravity = rb.gravityScale;
        originalExclusionLayers = rb.excludeLayers;
    }
    public void KillMe()
    {
        if (!dead)
        {
            dead = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.mass = deathMass;
            rb.velocity = Random.insideUnitCircle * deathVelocity;
            rb.gravityScale = deathGravity;
            int randomSign = Random.Range(-1, 2);
            float randomAngularSpin = Random.Range(angularSpinOnDeathBounds.x, angularSpinOnDeathBounds.y);
            if (randomSign <= 0)
            {
                randomAngularSpin *= -1;
            }
            rb.angularVelocity = randomAngularSpin;
            rb.excludeLayers = deathExclusionLayer;
            gameObject.layer = deathLayer;
        }
    }
    private void Update()
    {
        if(transform.position.y < yValueDestroy)
        {
            SharedGameObjectPool.Return(gameObject);
        }
    }
    public void OnRent()
    {
    }
    public void OnReturn()
    {
        dead = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.mass = originalMass;
        rb.gravityScale = originalGravity;
        rb.excludeLayers = originalExclusionLayers;
    }
}
