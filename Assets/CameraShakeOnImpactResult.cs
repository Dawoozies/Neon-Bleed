using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraShakeOnImpactResult : MonoBehaviour
{
    public float shakeAmplitude;
    public LayerMask impactLayerMask;
    public void ApplyShake(Vector3 velocity, RaycastHit2D[] results)
    {
        foreach (var hit in results)
        {
            if(LayerMaskUtility.IsInLayerMask(hit.collider.gameObject, impactLayerMask))
            {
                CameraControl.ins.CameraShakeAddAmplitude(shakeAmplitude);
            }
        }
    }
}
