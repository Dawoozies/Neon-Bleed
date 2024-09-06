using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitMotion;
using UnityEngine.UI;
public class ImageColorPaletteApply : MonoBehaviour
{
    ColorPalette ActiveColorPalette => StaticData.ins.ActiveColorPalette;
    ColorPalette _ActiveColorPalette;
    Image image;
    public int paletteIndex;
    public ColorPaletteMotionData motionData;
    protected virtual void Start()
    {
        image = GetComponent<Image>();
    }
    protected virtual void Update()
    {
        if(_ActiveColorPalette != ActiveColorPalette)
        {
            DoColorMotion();
            _ActiveColorPalette = ActiveColorPalette;
        }
    }
    protected virtual void DoColorMotion()
    {
        Color targetColor = ActiveColorPalette.colors[paletteIndex];
        LMotion.Create(image.color, targetColor, motionData.transitionTime)
            .WithDelay(motionData.delay)
            .WithEase(motionData.easing)
            .Bind(x => image.color = x);
    }
}
