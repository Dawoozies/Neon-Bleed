using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class Weapons : MonoBehaviour, IPoolCallbackReceiver
{
    //
    public List<int> weaponIndices = new();
    public int randomChoiceCount;
    public bool randomlyChooseWeaponOnStart;
    List<int> possibleWeaponChoices = new();
    public void ActivateWeapons()
    {
        if(randomlyChooseWeaponOnStart)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                possibleWeaponChoices.Add(i);
            }
            for (int i = 0; i < randomChoiceCount; i++)
            {
                int indexChoice = Random.Range(0, possibleWeaponChoices.Count);
                weaponIndices.Add(possibleWeaponChoices[indexChoice]);
                possibleWeaponChoices.RemoveAt(indexChoice);
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(weaponIndices.Contains(i));
        }
    }
    public void DeactivateAllWeapons()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }
    public void OnRent()
    {
    }
    public void OnReturn()
    {
        if(randomlyChooseWeaponOnStart)
            weaponIndices.Clear();
    }
}