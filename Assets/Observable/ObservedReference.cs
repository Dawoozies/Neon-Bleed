using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObservedReference<T> : ScriptableObject
{
    //keep reference to thing
    //expose setter
    public T Reference;
    protected List<IObserver<T>> observers = new();
    public void SetReference(T newRef)
    {
        //before changing we can fire off events for what previous reference was and new reference is
        T previousRef = Reference;
        Reference = newRef;
        //FIRST IN LAST OUT
        for (int i = observers.Count - 1; i >= 0; i--)
        {
            observers[i].OnSetReference(previousRef, newRef);
        }
    }
    public T GetReference()
    {
        return Reference;
    }
    public void RegisterObserver(IObserver<T> observer)
    {
        observers.Add(observer);
    }
    public void UnregsiterObserver(IObserver<T> observer)
    {
        if(observers.Contains(observer)) 
            observers.Remove(observer);
    }
}
public interface IObserver<T>
{
    public void OnSetReference(T previousRef, T newRef);
}