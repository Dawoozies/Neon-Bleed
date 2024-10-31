using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class UpgradeChoice : MonoBehaviour
{
    [ReorderableList] public GameObject[] upgradeChoices;
    public ObservedInt ObservedUpgradesLeft;
    public ObservedInt ActiveAngelCount;
    public int choicesPerUpgrade;
    public bool canDoUpgrade;
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
                for(int i = 0; i < choicesPerUpgrade; i++)
                {
                    GameObject upgradeChoice = SharedGameObjectPool.Rent(upgradeChoices[Random.Range(0, upgradeChoices.Length)]);
                    upgradeChoice.transform.SetParent(transform, false);
                }
            }
        }
    }
}
