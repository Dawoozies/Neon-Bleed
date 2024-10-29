using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ColorPaletteApply : MonoBehaviour
{
    protected ColorPalette ActiveColorPalette => StaticData.ins.ActiveColorPalette;
    protected ColorPalette _ActiveColorPalette;
    public ColorPaletteMotionData motionData;
    protected virtual void Update()
    {
        if (_ActiveColorPalette != ActiveColorPalette)
        {
            DoColorMotion();
            _ActiveColorPalette = ActiveColorPalette;
        }
    }
    protected virtual void DoColorMotion()
    {
    }
}
