using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponUpgrade", menuName = "Upgrades/WeaponUpgrade")]
public class WeaponUpgrade : Upgrade
{
    public Stats entityStats;
    public WeaponStats weaponStats;
    int weaponId => weaponStats.weaponId;
    List<int> shotAngles => weaponStats.shotAngles;
    public override void ActivateUpgrade()
    {
        entityStats.AddWeapon(weaponStats);
    }
}
