using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalEffectPlay : MonoBehaviour
{
    public int effectIndex;
    public int hitResultEmit;
    public void PlayEffect()
    {
        ExternalEffects.ins.PlayEffect(effectIndex, transform.position);
    }
    public void PlayEffectAtHitResults(RaycastHit2D[] results)
    {
        foreach (RaycastHit2D hit in results)
        {
            ExternalEffects.ins.EmitAtPosition(effectIndex, hit.point, hitResultEmit);
        }
    }
}
