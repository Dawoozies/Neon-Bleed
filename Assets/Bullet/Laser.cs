using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Laser : MonoBehaviour
{
    LaserEffect laserEffect;
    public UnityEvent onShoot;
    public UnityEvent<RaycastHit2D[]> onHit;
    public float cooldownTime;
    float cooldownTimer;
    public LayerMask laserHitMask;
    public LayerMask laserStopMask;
    Vector2 laserDir;
    public float laserMaxDistance;
    RaycastHit2D[] results;
    Vector2 laserEndPoint;
    private void Start()
    {
        laserEffect = GetComponentInChildren<LaserEffect>();
    }
    private void Update()
    {
        laserEffect.points[0].position = transform.position;
        laserEffect.points[1].position = MousePosition.ins.WorldPos;

        laserDir = (MousePosition.ins.WorldPos - transform.position).normalized;
        float maxDist = laserMaxDistance;
        RaycastHit2D laserEndHit = Physics2D.Raycast(transform.position, laserDir, laserMaxDistance, laserStopMask);
        if (laserEndHit.collider != null)
        {
            laserEffect.points[1].position = laserEndHit.point;
            maxDist = laserEndHit.distance;
        }

        if(laserEffect.IsFiring())
        {
            results = Physics2D.RaycastAll(transform.position, laserDir, maxDist, laserHitMask);
            if(results.Length > 0)
            {
                onHit?.Invoke(results);
            }
        }

        if (Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            cooldownTimer = cooldownTime;
            onShoot?.Invoke();
        }

        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}