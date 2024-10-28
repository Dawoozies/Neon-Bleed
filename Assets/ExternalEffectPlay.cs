using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ExternalEffectPlay : MonoBehaviour
{
    [ReorderableList] public List<int> effectsToPlay;
    public int effectIndex;
    public int hitResultEmit;
    public void PlayEffect()
    {
        foreach (int i in effectsToPlay)
        {
            ExternalEffects.ins.PlayEffect(i, transform.position);
        }
    }
    public void PlayEffectAtHitResults(RaycastHit2D[] results)
    {
        foreach (RaycastHit2D hit in results)
        {
            foreach(int i in effectsToPlay)
            {
                ExternalEffects.ins.EmitAtPosition(i, hit.point, hitResultEmit);
            }
        }
    }
}
