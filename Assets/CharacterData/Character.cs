using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Character : ScriptableObject, IInfo
{
    //observables
    //
    public virtual string GetDescription()
    {
        return "???";
    }
    public virtual string GetName()
    {
        return "???";
    }
}