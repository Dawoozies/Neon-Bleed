using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;
[CreateAssetMenu]
public class Location : ScriptableObject, IInfo
{
    public string locationName;
    public string locationDescription;
    public virtual void OnEnterLocation()
    {
    }
    public virtual void OnExitLocation()
    {
    }
    public virtual string GetName()
    {
        return locationName;
    }
    public virtual string GetDescription()
    {
        //string desc = locationDescription;
        //if(instancedCharacters.Length > 0)
        //{
        //    string charListDisplay = "\n";
        //    int index = 1;
        //    foreach(Character character in instancedCharacters)
        //    {
        //        charListDisplay += index == 1 ? "" : "\n";
        //        charListDisplay += $"{index}   )    " + character.GetName();
        //        index++;
        //    }
        //    desc += charListDisplay;
        //}
        //return desc;
        return locationDescription;
    }
}