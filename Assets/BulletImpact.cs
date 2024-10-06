using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    public float damage;
    public virtual void Impact()
    {
        ExternalEffects.ins.PlayEffect(1, transform.position);
        Destroy(gameObject);
    }
}
