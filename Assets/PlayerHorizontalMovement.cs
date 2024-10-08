using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour, IVelocityComponent
{
    public Vector2 v => movement;
    public Vector2 movement;

    public float moveSpeed;
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal") * moveSpeed;
    }
}
