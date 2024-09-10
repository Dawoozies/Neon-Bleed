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
    protected override void Start()
    {
        base.Start();
        startingSize = rectTransform.rect.size;
    }
    protected override void OnMouseOverEnter()
    {
        base.OnMouseOverEnter();
        if(inMenu)
        {
            rectTransform.SetParent(encapsulatingMenu.GetPriorityTransform());
        }
        LMotion.Create(rectTransform.position, rectTransform.position + hoverOverOffset, transitionTime)
        .WithEase(easing)
        .Bind(x => rectTransform.position = x);
        LMotion.Create(rectTransform.rect.size, rectTransform.rect.size + hoverOverSize, transitionTime)
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
        LMotion.Create(rectTransform.position, onScreenPosition, transitionTime)
        .WithEase(easing)
        .Bind(x => rectTransform.position = x);
        LMotion.Create(rectTransform.rect.size, startingSize, transitionTime)
            .WithEase(easing)
        .Bind((Vector2 x) => {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x.y);
        }
        );
    }
}