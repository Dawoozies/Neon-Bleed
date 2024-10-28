using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;
public class NestAttachImpact : MonoBehaviour, IPoolCallbackReceiver
{
    public GameObject nestInhabitantPrefab;
    public int amountToSpawn;
    public float maxAttachTime;
    float attachTime;
    public float damage;
    public float damageTime;
    BloodManager attachedBloodManager;
    bool attached = false;
    float t;
    Vector3 attachPoint;
    public UnityEvent onAttached;
    public UnityEvent onDetached;
    public virtual void Impact(Vector3 hitVelocity, RaycastHit2D[] results)
    {
        if (results.Length > 0)
        {
            foreach(var result in results)
            {
                if (result.collider.gameObject.TryGetComponent(out attachedBloodManager))
                {
                    attachedBloodManager.RegisterOnBloodDepletedCallback(OnAngelBloodDepletedHandler);
                    attached = true;
                    transform.parent = attachedBloodManager.transform;
                    attachPoint =  attachedBloodManager.transform.worldToLocalMatrix.MultiplyPoint(result.centroid);
                    onAttached?.Invoke();
                    return;
                }
            }
        }
        SharedGameObjectPool.Return(gameObject);
    }
    public virtual void OnAngelBloodDepletedHandler(BloodManager angelBloodManager)
    {
        //get rid of attached transform
        attachedBloodManager = null;
        attached = false;
        transform.parent = null;
        onDetached?.Invoke();

        SpawnInhabitants();

        SharedGameObjectPool.Return(gameObject);
    }
    public virtual void Update()
    {
        if (attached)
        {
            transform.localPosition = attachPoint;
            if (t > 0f)
            {
                t -= Time.deltaTime;
            }
            else
            {
                t = damageTime;
                DealDamage();
            }

            attachTime += Time.deltaTime;
            if(attachTime >= maxAttachTime)
            {
                attachedBloodManager.UnregisterOnBloodDepletedCallback(OnAngelBloodDepletedHandler);
                OnAngelBloodDepletedHandler(attachedBloodManager);
            }
        }
    }
    public virtual void DealDamage()
    {
        attachedBloodManager.IncreaseBleedIntensity(damage);
    }
    public virtual void SpawnInhabitants()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject newSpawn = SharedGameObjectPool.Rent(nestInhabitantPrefab);
            newSpawn.transform.position = transform.position;
            newSpawn.transform.right = Random.insideUnitCircle;
        }
    }

    public void OnRent()
    {
        attachTime = 0f;
    }
    public void OnReturn()
    {
    }
}
