using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    public Transform[] points;
    LineRenderer lineRenderer;
    ParticleSystem ps;
    ParticleSystem.MainModule main;
    public int maxParticles;
    public float laserPunchTime;
    public float laserPunchStrength;
    public Ease laserWidthEasing;
    public bool fireLaserDebug;
    MotionHandle laserMotionHandle;
    private void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.startWidth = 0f;
        lineRenderer.endWidth = 0f;
        ps = GetComponentInChildren<ParticleSystem>();
        main = ps.main;
    }
    private void Update()
    {
        ps.transform.right = points[1].position - points[0].position;

        if(lineRenderer.positionCount != points.Length)
            lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }

        if (fireLaserDebug)
        {
            Fire();
            fireLaserDebug = false;
        }

        ps.transform.position = points[points.Length - 1].position;
    }
    public void Fire()
    {
        laserMotionHandle = LMotion.Punch.Create(0f, laserPunchStrength, laserPunchTime)
            .WithEase(laserWidthEasing)
            .Bind(x => {
                lineRenderer.startWidth = x;
                lineRenderer.endWidth = x;
            });

        LMotion.Punch.Create(0, maxParticles, laserPunchTime)
            .WithEase(laserWidthEasing)
            .Bind(x => main.maxParticles = Mathf.FloorToInt(x));

        CameraControl.ins.CameraShakeAddAmplitude(laserPunchStrength);
    }
    public bool IsFiring()
    {
        return laserMotionHandle.IsActive() && lineRenderer.startWidth > 0.01f;
    }
}