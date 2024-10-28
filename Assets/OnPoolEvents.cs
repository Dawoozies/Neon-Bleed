using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;

public class OnPoolEvents : MonoBehaviour, IPoolCallbackReceiver
{
    public UnityEvent onRent;
    public UnityEvent onReturn;
    public void OnRent()
    {
        onRent?.Invoke();
    }
    public void OnReturn()
    {
        onReturn?.Invoke();
    }
}
