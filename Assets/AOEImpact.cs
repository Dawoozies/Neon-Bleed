using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class AOEImpact : MonoBehaviour
{
    public float damage;
    public AnimationCurve damageFalloff;
    public float maxDist;
    public virtual void Impact(Vector3 hitVelocity, RaycastHit2D[] results)
    {
        Collider2D[] areaResults = Physics2D.OverlapCircleAll(transform.position, maxDist);
        foreach (Collider2D areaResult in areaResults)
        {
            BloodManager bloodManager;
            if(areaResult.gameObject.TryGetComponent(out bloodManager))
            {
                float d = Vector2.Distance(transform.position, areaResult.gameObject.transform.position);
                bloodManager.IncreaseBleedIntensity(damage*damageFalloff.Evaluate(d/maxDist));
            }
        }
        SharedGameObjectPool.Return(gameObject);
    }
}
