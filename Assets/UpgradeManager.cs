using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UpgradeManager : MonoBehaviour
{
    public ObservedInt ObservedPlayerSoulPower;
    public ObservedInt UpgradesLeft;
    public ObservedUpgrade ObservedUpgradeChoice;
    public ObservedInt ActiveAngelCount;
    public int upgradesCollected;
    public int soulPowerRequiredForUpgrade;
    public int currentUpgradesLeft;
    public UnityEvent onPlayerCanUpgrade;
    public UnityEvent onUpgradeCompleted;
    private void Awake()
    {
        UpgradesLeft.SetReference(0);
        ObservedUpgradeChoice.SetReference(null);
    }
    private void OnEnable()
    {
        ObservedPlayerSoulPower.RegisterObserver(0, PlayerSoulPower_OnSetReference);
        ObservedUpgradeChoice.RegisterObserver(0, UpgradeChoice_OnSetReference);
        //ActiveAngelCount.RegisterObserver(0, ActiveAngelCount_OnSetReference);
    }
    private void OnDisable()
    {
        ObservedPlayerSoulPower.UnregisterObserver(0, PlayerSoulPower_OnSetReference);
        ObservedUpgradeChoice.UnregisterObserver(0, UpgradeChoice_OnSetReference);
    }
    void PlayerSoulPower_OnSetReference(int previousValue, int newValue)
    {
        int requirementForUpgrade = (upgradesCollected + 1)* soulPowerRequiredForUpgrade;
        Debug.Log($"Requirement For Upgrade = {requirementForUpgrade}");
        if(newValue >= requirementForUpgrade)
        {
            onPlayerCanUpgrade?.Invoke();
            UpgradesLeft.Increment();
        }
    }
    void UpgradeChoice_OnSetReference(Upgrade previousRef, Upgrade newRef)
    {
        upgradesCollected++;
        UpgradesLeft.Decrement();
        if (UpgradesLeft.GetReference() <= 0)
            onUpgradeCompleted?.Invoke();
    }
    private void Update()
    {
        currentUpgradesLeft = System.Math.DivRem(ObservedPlayerSoulPower.GetReference(), soulPowerRequiredForUpgrade, out _) - upgradesCollected;
    }
}