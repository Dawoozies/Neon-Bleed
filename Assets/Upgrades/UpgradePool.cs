using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UpgradePool", menuName = "Upgrades/UpgradePool")]
public class UpgradePool : ScriptableObject
{
    [ReorderableList] public Upgrade[] upgrades;
    public bool TryPickRandomUpgrades(int toPick, out List<Upgrade> upgradesPicked)
    {
        upgradesPicked = new List<Upgrade>();
        if (upgrades == null || upgrades.Length == 0)
            return false;

        int choicesLeft = toPick;
        int whileGuard = upgrades.Length;
        while (choicesLeft > 0)
        {
            int randomIndex = Random.Range(0, upgrades.Length);
            if (!upgradesPicked.Contains(upgrades[randomIndex]))
            {
                upgradesPicked.Add(upgrades[randomIndex]);
                choicesLeft--;
            }

            whileGuard--;
            if(whileGuard < 0)
                break;
        }

        return true;
    }
    public Upgrade GetRandomUpgrade()
    {
        return upgrades[Random.Range(0, upgrades.Length)];
    }
}
