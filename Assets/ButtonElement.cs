using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitMotion;
public class ButtonElement : Element
{
    Vector2 startingSize;
    public Vector3 hoverOverOffset;
    public Vector2 hoverOverSize;
    public float transitionTime;
    public Ease easing;
    protected MotionHandle[] onEnterMotionHandles;
    protected MotionHandle[] onExitMotionHandles;
    protected override void Start()
    {
        base.Start();
        startingSize = rectTransform.rect.size;
        onEnterMotionHandles = new MotionHandle[3];
        onExitMotionHandles = new MotionHandle[3];
    }
    protected override void OnMouseOverEnter()
    {
        base.OnMouseOverEnter();
        if(inMenu)
        {
            rectTransform.SetParent(encapsulatingMenu.GetPriorityTransform());
        }

        foreach(MotionHandle handle in onEnterMotionHandles)
        {
            if(handle.IsActive())
            {
                handle.Cancel();
            }
        }

        onEnterMotionHandles[(int)MotionType.Position] = LMotion.Create(rectTransform.localPosition, rectTransform.localPosition + hoverOverOffset, transitionTime)
        .WithEase(easing)
        .Bind(x => rectTransform.localPosition = x);
        onEnterMotionHandles[(int)MotionType.Size] = LMotion.Create(rectTransform.rect.size, rectTransform.rect.size + hoverOverSize, transitionTime)
        .WithEase(easing)
        .Bind((Vector2 x) => {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x.y);
        }
        );
    }
    protected override void OnMouseOverExit()
    {
        base.OnMouseOverExit();
        if (inMenu)
        {
            rectTransform.SetParent(encapsulatingMenu.GetElementsTransform());
        }

        foreach (MotionHandle handle in onExitMotionHandles)
        {
            if (handle.IsActive())
            {
                handle.Cancel();
            }
        }

        onExitMotionHandles[(int)MotionType.Position] = LMotion.Create(rectTransform.localPosition, onScreenPosition, transitionTime)
        .WithEase(easing)
        .Bind(x => rectTransform.localPosition = x);
        onExitMotionHandles[(int)MotionType.Size] = LMotion.Create(rectTransform.rect.size, startingSize, transitionTime)
            .WithEase(easing)
        .Bind((Vector2 x) => {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x.y);
        }
        );
    }
}