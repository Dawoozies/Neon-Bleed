using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCooldown : MonoBehaviour
{
    public ObservedFloat cooldownStat;
    public float reductionPerLevel;
    public void Upgrade()
    {
        cooldownStat.SetReference(cooldownStat.GetReference() + reductionPerLevel);
    }
}
