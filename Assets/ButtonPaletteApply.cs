using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonPaletteApply : ColorPaletteApply
{
    Button button;
    public int normalColorPaletteIndex = -1;
    public int highlightedColorPaletteIndex = -1;
    public int pressedColorPaletteIndex = -1;
    enum ButtonColor
    {
        NormalColor,
        HighlightedColor,
        PressedColor,
    }
    ColorBlock currentColorBlock;
    void Awake()
    {
        button = GetComponent<Button>();
        currentColorBlock = button.colors;
    }
    protected override void DoColorMotion()
    {
        ColorBlock buttonColorBlock = button.colors;
        if(normalColorPaletteIndex >= 0)
        {
            LMotion.Create(button.colors.normalColor, ActiveColorPalette.colors[normalColorPaletteIndex], motionData.transitionTime)
                .WithDelay(motionData.delay)
                .WithEase(motionData.easing)
                .Bind(x => {
                    currentColorBlock.normalColor = x;
                });
        }
        if (highlightedColorPaletteIndex >= 0)
        {
            LMotion.Create(button.colors.highlightedColor, ActiveColorPalette.colors[highlightedColorPaletteIndex], motionData.transitionTime)
                .WithDelay(motionData.delay)
                .WithEase(motionData.easing)
                .Bind(x => {
                    currentColorBlock.highlightedColor = x;
                });
        }
        if (pressedColorPaletteIndex >= 0)
        {
            LMotion.Create(button.colors.pressedColor, ActiveColorPalette.colors[pressedColorPaletteIndex], motionData.transitionTime)
                .WithDelay(motionData.delay)
                .WithEase(motionData.easing)
                .Bind(x => {
                    currentColorBlock.pressedColor = x;
                });
        }
    }
    protected override void Update()
    {
        base.Update();
        button.colors = currentColorBlock;
    }
}
