using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;
public class BloodRot : MonoBehaviour, IPoolCallbackReceiver
{
    public float bloodRotTime;
    float rotTimer;
    public UnityEvent<float> onRotUpdate;
    public void OnRent()
    {
        rotTimer = 0f;
    }
    public void OnReturn()
    {
    }
    void Update()
    {
        if(rotTimer < bloodRotTime)
        {
            rotTimer += Time.deltaTime;
            onRotUpdate?.Invoke(rotTimer/bloodRotTime);
        }
        else
        {
            SharedGameObjectPool.Return(gameObject);
        }
    }
}
