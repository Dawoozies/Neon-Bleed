using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocities : MonoBehaviour
{
    Rigidbody2D rb;
    IVelocityComponent[] velocityComponents;
    public bool ignoreTimeScale;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocityComponents = GetComponents<IVelocityComponent>();
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        foreach (var velocityComponent in velocityComponents)
        {
            rb.velocity += velocityComponent.v;
        }

        if(ignoreTimeScale)
        {
            rb.velocity *= TimeManager.ins.InverseTimeScaleMultiplier();
        }
    }
}
public interface IVelocityComponent
{
    public Vector2 v { get; }
}