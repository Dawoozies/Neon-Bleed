using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodAdrenaline : MonoBehaviour, IObserver<Upgrade>
{
    public int tier;
    public float timeScale;
    public Upgrade upgradeRequired;
    public ObservedUpgrade ObservedUpgradeChoice;
    public ObservedFloat PlayerBloodlust;
    float bloodlust => PlayerBloodlust.GetReference();
    public float bloodlustDrainPerSecond;
    public int OrderPriority => 1;
    void OnEnable()
    {
        ObservedUpgradeChoice.RegisterObserver(this);
    }
    void OnDisable()
    {
        ObservedUpgradeChoice.UnregisterObserver(this);
    }
    public void OnSetReference(Upgrade previousRef, Upgrade newRef)
    {
        if(newRef == upgradeRequired)
        {
            tier++;
        }
    }
    void Update()
    {
        if (tier <= 0)
            return;
        float bloodlustUsage = bloodlustDrainPerSecond * Time.unscaledDeltaTime;
        if (Input.GetMouseButton(1) && bloodlust > bloodlustUsage)
        {
            PlayerBloodlust.SetReference(bloodlust - bloodlustUsage);
            TimeManager.ins.RequestSlowTime(0.1f, timeScale);
        }
    }
}
