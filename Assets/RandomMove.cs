using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : PhysMovement, IVelocityComponent
{
    public float randomMoveTime;
    float randomMoveTimer;
    public float randomMoveSpeed;
    Vector2 randomDir;
    public Vector2 v => randomDir * randomMoveSpeed;
    public override void Update()
    {
        base.Update();
        if (randomMoveTimer > 0)
        {
            randomMoveTimer -= Time.deltaTime;
        }
        else
        {
            randomMoveTimer = randomMoveTime;
            randomDir = Random.insideUnitCircle;
        }
    }
}
