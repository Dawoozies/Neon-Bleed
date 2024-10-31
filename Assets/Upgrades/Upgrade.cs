using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Upgrade : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public virtual void ActivateUpgrade()
    {
    }
    public virtual string GetUpgradeName()
    {
        return upgradeName;
    }
    public virtual string GetUpgradeDescription()
    {
        return description;
    }
}
//upgrades
// - weapon unlock
//      * weapon index to unlock
//      *
//      * reference to player stats