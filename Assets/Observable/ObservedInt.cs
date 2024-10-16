using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ObservedInt", menuName = "ObservedReferences/ObservedInt", order = 1)]

public class ObservedInt : ObservedReference<int>
{
    public void Increment()
    {
        int newValue = GetReference() + 1;
        SetReference(newValue);
    }
    public void Decrement()
    {
        int newValue = GetReference() - 1;
        SetReference(newValue);
    }
}
