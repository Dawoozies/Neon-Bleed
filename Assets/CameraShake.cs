using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmplitude;
    public void ApplyShake()
    {
        CameraControl.ins.CameraShakeAddAmplitude(shakeAmplitude);
    }
}
