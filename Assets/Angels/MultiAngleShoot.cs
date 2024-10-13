using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAngleShoot : AutoShoot
{
    public float[] angles;
    public bool allAtOnce;
    public int index;
    public float shotSpeed;
    Vector2[] shotDirs;
    private void Start()
    {
        shotDirs = new Vector2[angles.Length];
        for (int i = 0; i < shotDirs.Length; i++)
        {
            shotDirs[i].x = Mathf.Cos(angles[i] * Mathf.Deg2Rad);
            shotDirs[i].y = Mathf.Sin(angles[i] * Mathf.Deg2Rad);
        }
    }
    public override void Shoot()
    {
        if(allAtOnce)
        {
            foreach(var shotDir in shotDirs)
            {
                BulletManager.ins.FireBullet(bulletIndex, shotDir * shotSpeed, transform.position);
            }
        }
        else
        {
            BulletManager.ins.FireBullet(bulletIndex, shotDirs[index]*shotSpeed, transform.position);
            index++;
            index = index % shotDirs.Length;
        }
    }
}