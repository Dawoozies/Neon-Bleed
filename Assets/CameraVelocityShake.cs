using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVelocityShake : MonoBehaviour
{
    public float shakeAmplitude;
    public float maxVelocity;
    public AnimationCurve velocityShakeCurve;
    public void ApplyShake(Vector3 velocity, RaycastHit2D[] results)
    {
        float velocityLerpValue = velocity.magnitude/maxVelocity;
        if(velocity.magnitude > maxVelocity)
        {
            velocityLerpValue = 1f;
        }

        CameraControl.ins.CameraShakeAddAmplitude(shakeAmplitude * velocityShakeCurve.Evaluate(velocityLerpValue));
    }
}
