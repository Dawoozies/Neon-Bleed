using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour, IVelocityComponent
{
    public Vector2 v => jump;
    public Vector2 jump;

    //if air time > coyoteTime then no jump
    public float jumpSpeed;
    public float coyoteTime;
    public bool canJump;

    public AnimationCurve jumpHeldCurve;
    public float jumpHeldTime;
    public float jumpHeldTimeMax;
    public void OnAirTime(float airTime)
    {
        canJump = airTime <= coyoteTime;
    }
    void Update()
    {
        if (canJump && Input.GetButton("Jump"))
        {
            jumpHeldTime = 0f;
        }

        if(Input.GetButton("Jump"))
        {
            jumpHeldTime += Time.deltaTime;
        }
        else
        {
            jumpHeldTime = jumpHeldTimeMax;
        }

        jump.y = jumpSpeed * jumpHeldCurve.Evaluate(jumpHeldTime/jumpHeldTimeMax);
    }
}
