using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UpgradeHandler : MonoBehaviour, IObserver<Upgrade>
{
    public bool upgradeActive;
    public ObservedUpgrade ObservedUpgradeChoice;
    public int OrderPriority => 1;
    public virtual void OnEnable()
    {
        ObservedUpgradeChoice.RegisterObserver(this);
    }
    public virtual void OnDisable()
    {
        ObservedUpgradeChoice.RegisterObserver(this);
    }
    public virtual void OnSetReference(Upgrade previousRef, Upgrade newRef)
    {
    }
}