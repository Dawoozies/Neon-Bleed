using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    AutoShoot[] weapons;
    private void Awake()
    {
        weapons = GetComponentsInChildren<AutoShoot>();
    }
    public void ShootAllWeapons()
    {
        foreach (var weapon in weapons)
        {
            weapon.ManuallyShoot();
        }
    }
}
