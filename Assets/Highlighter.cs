using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitMotion;
public class Highlighter : MonoBehaviour
{
    protected GameObject SelectedObject => Elements.ins.selectedObject;
    protected GameObject _SelectedObject;
    protected RectTransform rectTransform;
    [SerializeField] protected Vector2 size;
    [SerializeField] protected float transitionTime;
    [SerializeField] protected Ease easing;
    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    protected virtual void Update()
    {
        if(_SelectedObject != SelectedObject)
        {
            RectTransform targetRect;
            if(SelectedObject.TryGetComponent(out targetRect))
            {
                int newSiblingIndex = targetRect.GetSiblingIndex() - 1;
                if(targetRect.GetSiblingIndex() == 0)
                {
                    newSiblingIndex = 0;
                }
                rectTransform.SetSiblingIndex(newSiblingIndex);
                //do the move to the new selected object
                LMotion.Create(rectTransform.position, targetRect.position, transitionTime)
                    .WithEase(easing)
                    .Bind(x => rectTransform.position = x);
                LMotion.Create(rectTransform.rect.width, targetRect.rect.width * targetRect.lossyScale.x + size.x, transitionTime)
                    .WithEase(easing)
                    .Bind(x => rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x));
                LMotion.Create(rectTransform.rect.height, targetRect.rect.height * targetRect.lossyScale.y + size.y, transitionTime)
                    .WithEase(easing)
                    .Bind(x => rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x));
            }
            _SelectedObject = SelectedObject;
        }
    }
    protected virtual void DoPositionMotion()
    {
    }
    protected virtual void DoRotationMotion() 
    { 
    }
}