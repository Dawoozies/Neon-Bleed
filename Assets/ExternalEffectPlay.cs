using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalEffectPlay : MonoBehaviour
{
    public int effectIndex;
    public void PlayEffect()
    {
        ExternalEffects.ins.PlayEffect(effectIndex, transform.position);
    }
}
