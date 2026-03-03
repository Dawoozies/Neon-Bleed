using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using uPools;

public class UpgradeChoice : MonoBehaviour
{
    [ReorderableList] public GameObject[] upgradeChoices;
    public ObservedInt ObservedUpgradesLeft;
    public ObservedInt ActiveAngelCount;
    public int choicesPerUpgrade;
    public bool canDoUpgrade;
    public UpgradePool pool;
    private void OnEnable()
    {
        ObservedUpgradesLeft.RegisterObserver(0, UpgradesLeft_OnSetReference);
    }
    private void OnDisable()
    {
        ObservedUpgradesLeft.UnregisterObserver(0, UpgradesLeft_OnSetReference);
    }
    void UpgradesLeft_OnSetReference(int previousValue, int newValue)
    {
        if(newValue < previousValue)
        {
            canDoUpgrade = false;
        }
    }
    private void Update()
    {
        if(!canDoUpgrade)
        {
            canDoUpgrade =
                ObservedUpgradesLeft.GetReference() > 0
                && ActiveAngelCount.GetReference() == 0
                && transform.childCount == 0;
            if(canDoUpgrade)
            {
                var toPickFrom = pool.upgrades.ToList();
                for(int i = 0; i < choicesPerUpgrade; i++)
                {
                    UpgradeSelection upgradeSelection = SharedGameObjectPool.Rent(upgradeChoices[Random.Range(0, upgradeChoices.Length)]).GetComponent<UpgradeSelection>();
                    int selectedIndex = Random.Range(0, toPickFrom.Count);
                    upgradeSelection.upgrade = toPickFrom[selectedIndex];
                    upgradeSelection.InitialSetup();
                    upgradeSelection.UpgradeTextMotion();
                    upgradeSelection.InvokeOnRent();
                    toPickFrom.RemoveAt(selectedIndex);
                    upgradeSelection.transform.SetParent(transform, false);
                }
            }
        }
    }
}
