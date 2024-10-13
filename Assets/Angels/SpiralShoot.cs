using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;
public class SpiralShoot : AutoShoot
{
    public float currentAngle;
    public float angleIncreaseSpeed;
    public bool pingPong;
    Vector2 shotDir;
    public float shotSpeed;
    protected override void Update()
    {
        currentAngle += angleIncreaseSpeed * Time.deltaTime;

        if(pingPong)
        {
            if(currentAngle >= 360f || currentAngle <= 0f)
            {
                angleIncreaseSpeed = -angleIncreaseSpeed;
            }
        }
        else
        {
            if(angleIncreaseSpeed > 0f && currentAngle >= 360f)
            {
                currentAngle = 0f;
            }
            if(angleIncreaseSpeed < 0f && currentAngle <= 0f)
            {
                currentAngle = 360f;
            }
        }

        base.Update();
    }
    public override void Shoot()
    {
        Debug.Log("SHOOT SHOOT SHOOT");
        shotDir.x = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        shotDir.y = Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        BulletManager.ins.FireBullet(bulletIndex, shotDir*shotSpeed, transform.position);
    }
}