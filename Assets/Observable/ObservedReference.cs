using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ObservedReference<T> : ScriptableObject
{
    //keep reference to thing
    //expose setter
    protected T Reference;
    protected Dictionary<int, List<IObserver<T>>> observerPriorityLookUp = new();
    protected Dictionary<int, List<Action<T, T>>> actionPriorityLookUp = new();
    public virtual void SetReference(T newRef)
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
        foreach (var key in actionPriorityLookUp.Keys)
        {
            for (int i = actionPriorityLookUp[key].Count - 1; i >= 0; i--)
            {
                actionPriorityLookUp[key][i].Invoke(previousRef, newRef);
            }
        }
    }
    public T GetReference()
    {
        return Reference;
    }
    public virtual void RegisterObserver(IObserver<T> observer)
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
    public virtual void RegisterObserver(int OrderPriority, Action<T,T> a)
    {
        if (actionPriorityLookUp.ContainsKey(OrderPriority))
        {
            actionPriorityLookUp[OrderPriority].Add(a);
        }
        else
        {
            actionPriorityLookUp.Add(OrderPriority, new List<Action<T, T>> { a });
        }
    }
    public virtual void UnregisterObserver(IObserver<T> observer)
    {
        if(observerPriorityLookUp.ContainsKey(observer.OrderPriority))
        {
            if (observerPriorityLookUp[observer.OrderPriority].Contains(observer))
                observerPriorityLookUp[observer.OrderPriority].Remove(observer);
        }
    }
    public virtual void UnregisterObserver(int OrderPriority, Action<T,T> a)
    {
        if(actionPriorityLookUp.ContainsKey(OrderPriority))
        {
            if (actionPriorityLookUp[OrderPriority].Contains(a))
                actionPriorityLookUp[OrderPriority].Remove(a);
        }
    }
}
public interface IObserver<T>
{
    public int OrderPriority { get; }
    public void OnSetReference(T previousRef, T newRef);
}