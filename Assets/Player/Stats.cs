using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "EntityStats")]
public class Stats : ScriptableObject
{
    public float damageMultiplier;
    [ReorderableList] public List<WeaponStats> weaponsUnlocked = new();
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
        weaponsUnlocked = new List<WeaponStats>();
        foreach (WeaponStats weapon in toCopyFrom.weaponsUnlocked)
        {
            WeaponStats weaponCopy = new WeaponStats();
            weaponCopy.weaponId = weapon.weaponId;
            weaponCopy.CopyAngles(weapon);
            weaponCopy.shotAngles.Sort();
            weaponsUnlocked.Add(weaponCopy);
        }
    }
    public void ChangeDamageMultiplier(float toAdd)
    {
        damageMultiplier += toAdd;
        FireOnValidate();
    }
    public void AddWeapon(WeaponStats newWeaponStats)
    {
        bool isUnlocked = WeaponIsUnlocked(newWeaponStats.weaponId);
        if(isUnlocked)
        {
            //then just add the shot angles
            foreach (int angle in newWeaponStats.shotAngles)
            {
                AddShotAngle(newWeaponStats.weaponId, angle);
            }
        }
        else
        {
            WeaponStats newWeapon = new WeaponStats();
            newWeapon.weaponId = newWeaponStats.weaponId;
            newWeapon.CopyAngles(newWeaponStats);
            weaponsUnlocked.Add(newWeapon);
        }
        FireOnValidate();
    }
    public void AddShotAngle(int weaponId, int angle)
    {
        //foreach (var weaponStats in weaponsUnlocked)
        //{
        //    if(weaponStats.weaponId == weaponId)
        //    {
        //        if (weaponStats.shotAngles.Contains(angle))
        //            return;
        //        weaponStats.shotAngles.Add(angle);
        //    }
        //}
        foreach (var weaponStats in weaponsUnlocked)
        {
            if(weaponStats.weaponId == weaponId)
            {
                int angleToAdd = angle;
                int whileBreaker = 100;
                weaponStats.shotAngles.Sort();
                while(weaponStats.shotAngles.Contains(angleToAdd))
                {
                    if(angleToAdd > 0)
                    {
                        angleToAdd++;
                    }
                    if(angleToAdd < 0)
                    {
                        angleToAdd--;
                    }
                    if(angleToAdd == 0)
                    {
                        (int, int) posNegCount = weaponStats.PositiveNegativeAngleCount();
                        if(posNegCount.Item1 >= posNegCount.Item2)
                        {
                            //take lowest neg angle and minus one
                            angleToAdd = weaponStats.shotAngles[0]--;
                        }
                        else
                        {
                            //take largest pos angle and add one
                            angleToAdd = weaponStats.shotAngles[weaponStats.shotAngles.Count - 1]++;
                        }
                    }
                    whileBreaker--;
                    if (whileBreaker <= 0)
                        break;
                }
                weaponStats.shotAngles.Add(angleToAdd);
            }
        }
        FireOnValidate();
    }
    void FireOnValidate()
    {
        foreach (var action in onValidateActions)
        {
            action();
        }
    }
    public List<int> GetShotAngles(int weaponId)
    {
        foreach (var weaponStats in weaponsUnlocked)
        {
            if(weaponStats.weaponId == weaponId)
            {
                Debug.Log($"Getting shot angles for {weaponId}");
                return weaponStats.shotAngles;
            }
        }
        return null;
    }
    public bool WeaponIsUnlocked(int weaponId)
    {
        foreach (var weaponStats in weaponsUnlocked)
        {
            if (weaponStats.weaponId == weaponId)
            {
                return true;
            }
        }
        return false;
    }
}
[Serializable]
public class WeaponStats
{
    public int weaponId;
    [ReorderableList] public List<int> shotAngles = new();
    public void CopyAngles(WeaponStats toCopyFrom)
    {
        shotAngles.Clear();
        foreach (int angle in toCopyFrom.shotAngles)
        {
            shotAngles.Add(angle);
        }
    }
    public (int, int) PositiveNegativeAngleCount()
    {
        (int, int) posNegCount = new();
        foreach (int angle in shotAngles)
        {
            if(angle >= 0)
            {
                posNegCount.Item1++;
            }
            else
            {
                posNegCount.Item2++;
            }
        }
        return posNegCount;
    }
}