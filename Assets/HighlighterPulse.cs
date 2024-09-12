using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighlighterPulse : Highlighter
{
    public float delay;
    public Vector2 startingSize;
    public float pulseTransitionTime;
    Image image;
    Color color;
    public Vector2 alphaValues;

    MotionHandle mainMotionHandle;
    protected override void Start()
    {
        base.Start();
        image = GetComponent<Image>();
    }
    protected override void DoSizeMotion(RectTransform targetRect)
    {
        if (mainMotionHandle.IsActive())
        {
            return;
        }
        Vector2 targetSize = targetRect.sizeDelta + size;
        //targetSize.Scale(targetRect.lossyScale);
        Vector2 startSize = targetRect.sizeDelta + startingSize;
        //startSize.Scale(targetRect.lossyScale);
        mainMotionHandle = LMotion.Create(startingSize, targetSize, pulseTransitionTime)
            .WithDelay(delay)
            .WithEase(easing)
            .Bind(x => {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x.x);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x.y);
            });

        LMotion.Create(alphaValues.x, alphaValues.y, pulseTransitionTime)
            .WithDelay(delay)
            .WithEase(easing)
            .Bind(x => {
                color = image.color;
                color.a = x;
                image.color = color;
            });
    }
}
