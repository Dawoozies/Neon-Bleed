using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class BulletManager : MonoBehaviour
{
    public static BulletManager ins;
    private void Awake()
    {
        ins = this;
    }
    [ReorderableList] public GameObject[] bulletPrefabs;
    public void FireBullet(int index, Vector3 v, Vector3 pos)
    {
        GameObject bullet = SharedGameObjectPool.Rent(bulletPrefabs[index]);
        PhysMovement physMovement = bullet.GetComponentInChildren<PhysMovement>();
        bullet.transform.position = pos;
        physMovement.SetVelocity(v);
    }
}
