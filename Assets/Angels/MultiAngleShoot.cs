using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MultiAngleShoot : AutoShoot
{
    public float[] angles;
    public bool allAtOnce;
    public int index;
    public float shotSpeed;
    Vector2[] shotDirs;
    private void Start()
    {
        RecalculateShotDirs();
    }
    private void OnValidate()
    {
        RecalculateShotDirs();
    }
    public override void Shoot()
    {
        if(allAtOnce)
        {
            foreach(var shotDir in shotDirs)
            {
                Vector2 projectedDir = transform.right*shotDir.x + transform.up*shotDir.y;
                BulletManager.ins.FireBullet(bulletIndex, projectedDir * shotSpeed, transform.position);
            }
        }
        else
        {
            Vector2 projectedDir = transform.right * shotDirs[index].x + transform.up * shotDirs[index].y;
            BulletManager.ins.FireBullet(bulletIndex, projectedDir * shotSpeed, transform.position);
            index++;
            index = index % shotDirs.Length;
        }
    }
    public virtual void RecalculateShotDirs()
    {
        shotDirs = new Vector2[angles.Length];
        for (int i = 0; i < shotDirs.Length; i++)
        {
            shotDirs[i].x = Mathf.Cos(angles[i] * Mathf.Deg2Rad);
            shotDirs[i].y = Mathf.Sin(angles[i] * Mathf.Deg2Rad);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (shotDirs == null || shotDirs.Length == 0)
            return;
        Gizmos.color = Color.green;
        for (int i = 0; i < shotDirs.Length; i++)
        {
            Vector3 projectedDir = transform.right * shotDirs[i].x + transform.up * shotDirs[i].y;
            Vector3 shotShift = projectedDir * 20f;
            Gizmos.DrawLine(transform.position, transform.position + shotShift);
            Handles.Label(transform.position + shotShift, $"{angles[i]}°");
        }
    }
#endif

}