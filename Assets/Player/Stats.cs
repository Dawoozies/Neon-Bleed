using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "EntityStats")]
public class Stats : ScriptableObject
{
    public float damageMultiplier;
    [ReorderableList] public List<int> weaponsUnlocked = new();
    [ReorderableList] public List<int> shotAngles = new();
    List<Action> onValidateActions = new();
    public void RegisterOnValidateCallback(Action a)
    {
        onValidateActions.Add(a);
    }
    public void UnregisterOnValidateCallback(Action a) 
    { 
        if(onValidateActions.Contains(a))
            onValidateActions.Remove(a);
    }
    public void CopyStats(Stats toCopyFrom)
    {
        damageMultiplier = toCopyFrom.damageMultiplier;
        weaponsUnlocked = toCopyFrom.weaponsUnlocked;
        shotAngles = toCopyFrom.shotAngles;
    }
    public void ChangeDamageMultiplier(float toAdd)
    {
        damageMultiplier += toAdd;
        FireOnValidate();
    }
    public void AddWeapon(int weaponId)
    {
        if (weaponsUnlocked.Contains(weaponId))
            return;
        weaponsUnlocked.Add(weaponId);
        FireOnValidate();
    }
    public void AddShotAngle(int angle)
    {
        if (shotAngles.Contains(angle))
            return;
        shotAngles.Add(angle);
        FireOnValidate();
    }
    void FireOnValidate()
    {
        foreach (var action in onValidateActions)
        {
            action();
        }
    }
}