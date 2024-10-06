using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl ins;
    private void Awake()
    {
        ins = this;
    }
    public float shakeAmplitudeZeroTime;
    float shake_v;
    public float shakeAmplitude;
    public Camera mainCamera;
    public void CameraShakeAddAmplitude(float amplitude)
    {
        shakeAmplitude += amplitude;
    }
    private void Update()
    {
        mainCamera.transform.localPosition = Vector3.zero + (Vector3)Random.insideUnitCircle*shakeAmplitude;

        shakeAmplitude = Mathf.SmoothDamp(shakeAmplitude, 0f, ref shake_v, shakeAmplitudeZeroTime);
    }
}