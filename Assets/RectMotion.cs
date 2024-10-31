using LitMotion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using uPools;

public class RectMotion : MonoBehaviour, IPoolCallbackReceiver
{
    public float motionTime;
    public Ease ease;
    public float loopTime;
    public Ease loopEase;
    public Vector2 closedSize;
    public Vector2 openSize;
    public Vector2 loopSize;
    public Vector2 loopOffset;
    RectTransform rectTransform;
    CompositeMotionHandle motionHandles;
    Vector2 loopPos;
    public UnityEvent onMotionCompleted;
    private void Awake()
    {
        motionHandles = new CompositeMotionHandle();
        rectTransform = GetComponent<RectTransform>();
        openSize = rectTransform.rect.size;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, closedSize.x);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, closedSize.y);
    }
    public void Open()
    {
        LMotion.Create(rectTransform.rect.size, openSize, motionTime)
            .WithEase(ease)
            .WithOnComplete(() => { 
                StartLoop();
                onMotionCompleted?.Invoke();
            })
            .Bind(x => {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x.x);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x.y);
            })
            .AddTo(motionHandles);
    }
    void StartLoop()
    {
        loopPos = transform.localPosition;
        LMotion.Create(openSize, loopSize, loopTime)
            .WithEase(loopEase)
            .WithLoops(-1, LoopType.Yoyo)
            .Bind(x => {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x.x);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x.y);
            })
            .AddTo(motionHandles);
        LMotion.Create(-loopOffset, loopOffset, loopTime)
            .WithEase(loopEase)
            .WithLoops(-1, LoopType.Yoyo)
            .Bind(x => transform.localPosition = loopPos + x)
            .AddTo(motionHandles);
    }
    public void Close()
    {
        motionHandles.Cancel();
        LMotion.Create(rectTransform.rect.size, closedSize, motionTime)
            .WithEase(ease)
            .Bind(x => {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x.x);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, x.y);
            })
            .AddTo(motionHandles);
    }
    private void OnDestroy()
    {
    }
    public void OnRent()
    {

    }
    public void OnReturn()
    {
    }
}
