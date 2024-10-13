using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererPaletteApply : MonoBehaviour
{
    ColorPalette ActiveColorPalette => StaticData.ins.ActiveColorPalette;
    ColorPalette _ActiveColorPalette;
    SpriteRenderer spriteRenderer;
    public int paletteIndex;
    public ColorPaletteMotionData motionData;
    Color color;
    public float intensityMultiplier;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        LMotion.Create(spriteRenderer.color, targetColor * intensityMultiplier, motionData.transitionTime)
            .WithDelay(motionData.delay)
            .WithEase(motionData.easing)
            .Bind(x => {
                spriteRenderer.color = x;
            });
    }
}
