using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorPaletteApply : MonoBehaviour
{
    ColorPalette ActiveColorPalette => StaticData.ins.ActiveColorPalette;
    ColorPalette _ActiveColorPalette;
    Camera mainCamera;
    public int paletteIndex;
    public ColorPaletteMotionData motionData;
    Color color;
    public float intensityMultiplier;
    protected virtual void Start()
    {
        mainCamera = GetComponent<Camera>();
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
        LMotion.Create(mainCamera.backgroundColor, targetColor*intensityMultiplier, motionData.transitionTime)
            .WithDelay(motionData.delay)
            .WithEase(motionData.easing)
            .Bind(x => {
                mainCamera.backgroundColor = x;
            });
    }
}
