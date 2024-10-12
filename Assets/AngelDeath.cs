using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelDeath : MonoBehaviour
{
    bool dead;
    Rigidbody2D rb;
    public float deathMass;
    public float deathGravity;
    public LayerMask deathExclusionLayer;
    public float deathVelocity;
    public Vector2 angularSpinOnDeathBounds;
    public float yValueDestroy;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        }
    }
    private void Update()
    {
        if(transform.position.y < yValueDestroy)
        {
            Destroy(gameObject);
        }
    }
}
