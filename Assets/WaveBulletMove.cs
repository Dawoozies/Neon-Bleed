using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaveBulletMove : BulletMove, IVelocityComponent
{
    public float moveDrift;
    public AnimationCurve waveCurve;
    float lerpValue;
    Vector2 moveDir;
    public Vector2 v => movement;
    Vector2 movement;
    public override void OnReturn()
    {
        lerpValue = 0f;
    }
    public override void SetVelocity(Vector3 v)
    {
        moveDir = v;
    }
    public override void Update()
    {
        base.Update();
        lerpValue += Time.deltaTime;
        movement = moveDir * (moveDrift + waveCurve.Evaluate(lerpValue));
    }
}