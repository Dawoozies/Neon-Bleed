using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NestAttachImpact : MonoBehaviour
{
    public float damage;
    public float damageTime;
    BloodManager attachedBloodManager;
    bool attached = false;
    float t;
    public virtual void Impact(Vector3 hitVelocity, RaycastHit2D[] results)
    {
        if(results.Length > 0)
        {
            foreach(var result in results)
            {
                if (result.collider.gameObject.TryGetComponent(out attachedBloodManager))
                {
                    attachedBloodManager.RegisterOnBloodDepletedCallback(OnAngelBloodDepletedHandler);
                    attached = true;
                    break;
                }
            }
        }
    }
    public virtual void OnAngelBloodDepletedHandler(BloodManager angelBloodManager)
    {
        //get rid of attached transform
        attachedBloodManager = null;
        attached = false;
    }
    public virtual void Update()
    {
        if(attached)
        {
            if (t > 0f)
            {
                t -= Time.deltaTime;
            }
            else
            {
                t = damageTime;
                DealDamage();
            }
        }
    }
    public virtual void DealDamage()
    {
        attachedBloodManager.IncreaseBleedIntensity(damage);
    }
}
