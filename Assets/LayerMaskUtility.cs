using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class LayerMaskUtility
{
    public static bool IsInLayerMask(GameObject obj, LayerMask mask) => (mask.value & (1 << obj.layer)) != 0;
}
