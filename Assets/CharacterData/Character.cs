using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Character : ScriptableObject, IInfo
{
    //observables
    //
    public string description;
    public Color normalColor;
    public Color hoverOverColor;
    public Color pressedColor;
    public bool isBold;
    public virtual string GetDescription()
    {
        return description;
    }
    public virtual string GetName()
    {
        string value = name + this.GetInstanceID();
        return value;
    }
}