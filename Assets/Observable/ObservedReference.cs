using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObservedReference<T> : ScriptableObject
{
    //keep reference to thing
    //expose setter
    public T Reference;
    protected Dictionary<int, List<IObserver<T>>> observerPriorityLookUp = new();
    public void SetReference(T newRef)
    {
        //before changing we can fire off events for what previous reference was and new reference is
        T previousRef = Reference;
        Reference = newRef;
        //FIRST IN LAST OUT
        foreach (var key in observerPriorityLookUp.Keys)
        {
            for (int i = observerPriorityLookUp[key].Count - 1; i >= 0; i--)
            {
                observerPriorityLookUp[key][i].OnSetReference(previousRef, newRef);
            }
        }
    }
    public T GetReference()
    {
        return Reference;
    }
    public void RegisterObserver(IObserver<T> observer)
    {
        if(observerPriorityLookUp.ContainsKey(observer.OrderPriority))
        {
            observerPriorityLookUp[observer.OrderPriority].Add(observer);
        }
        else
        {
            observerPriorityLookUp.Add(observer.OrderPriority, new List<IObserver<T>> { observer });
        }
    }
    public void UnregsiterObserver(IObserver<T> observer)
    {
        if(observerPriorityLookUp.ContainsKey(observer.OrderPriority))
        {
            if (observerPriorityLookUp[observer.OrderPriority].Contains(observer))
                observerPriorityLookUp[observer.OrderPriority].Remove(observer);
        }
    }
}
public interface IObserver<T>
{
    public int OrderPriority { get; }
    public void OnSetReference(T previousRef, T newRef);
}