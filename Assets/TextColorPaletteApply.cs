using LitMotion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextColorPaletteApply : MonoBehaviour
{
    ColorPalette ActiveColorPalette => StaticData.ins.ActiveColorPalette;
    ColorPalette _ActiveColorPalette;
    TextMeshProUGUI textMesh;
    public int paletteIndex;
    public ColorPaletteMotionData motionData;
    Color color;
    protected virtual void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
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
        Color targetColor = ActiveColorPalette.colors[paletteIndex];
        LMotion.Create(textMesh.color, targetColor, motionData.transitionTime)
            .WithDelay(motionData.delay)
            .WithEase(motionData.easing)
            .Bind(x => {
                color = x;
                color.a = textMesh.color.a;
                textMesh.color = color;
            });
    }
}
