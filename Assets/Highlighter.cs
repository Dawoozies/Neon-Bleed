using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitMotion;
public class Highlighter : MonoBehaviour
{
    protected RectTransform rectTransform;
    [SerializeField] protected Vector2 size;
    [SerializeField] protected float transitionTime;
    [SerializeField] protected Ease easing;
    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Elements.ins.RegisterSelectedObjectChangeCallback(OnSelectedObjectChanged, 0);
    }
    protected virtual void OnDestroy()
    {
        Elements.ins.RemoveSelectedObjectChangeCallback(OnSelectedObjectChanged, 0);
    }
    protected virtual void Update()
    {
    }
    protected virtual void OnSelectedObjectChanged(GameObject selectedObject)
    {
        RectTransform targetRect;
        if (selectedObject.TryGetComponent(out targetRect))
        {
            rectTransform.SetParent(targetRect.parent);
            //ensure we follow the selected object to where it goes
            int newSiblingIndex = targetRect.GetSiblingIndex() - 1;
            if (targetRect.GetSiblingIndex() == 0)
            {
                newSiblingIndex = 0;
            }
            rectTransform.SetSiblingIndex(newSiblingIndex);
            //do the move to the new selected object
            DoPositionMotion(targetRect);
            DoSizeMotion(targetRect);
        }
    }
    protected virtual void DoPositionMotion(RectTransform targetRect)
    {
        LMotion.Create(rectTransform.position, targetRect.position, transitionTime)
            .WithEase(easing)
            .Bind(x => rectTransform.position = x);
    }
    protected virtual void DoRotationMotion() 
    { 
    }
    protected virtual void DoSizeMotion(RectTransform targetRect)
    {
        Vector2 targetSize = targetRect.rect.size + size;
        targetSize.Scale(targetRect.lossyScale);
        LMotion.Create(rectTransform.rect.size, targetSize, transitionTime)
            .WithEase(easing)
            .Bind(x =>
            {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x.x);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x.y);
            });
    }
}