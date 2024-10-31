using LitMotion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using uPools;
public class LoopTextMotion : MonoBehaviour
{
    TMP_Text text;
    RectTransform rectTransform;
    [Header("Fade In")]
    public float fadeInTime;
    public Ease fadeInEase;
    public Vector2 fadeInOffset;
    [Header("Fade Out")]
    public float fadeOutTime;
    public Ease fadeOutEase;
    public Vector2 fadeOutOffset;
    [Header("Loop")]
    public float loopTime;
    public Ease loopEase;
    public LoopType loopType;
    public Vector2 charOffsetMin, charOffsetMax;
    Vector2 mainPosition;
    public Gradient gradient;
    CompositeMotionHandle handles;
    private void Awake()
    {
        handles = new CompositeMotionHandle();
        text = GetComponent<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
        mainPosition = rectTransform.anchoredPosition;
        text.color = Color.clear;
    }
    public void Open()
    {
        LMotion.Create(Color.clear, gradient.Evaluate(0), fadeInTime)
            .WithEase(fadeInEase)
            .Bind(x => text.color = x)
            .AddTo(handles);
        LMotion.Create(mainPosition + fadeInOffset, mainPosition, fadeInTime)
            .WithEase(fadeInEase)
            .Bind(x => rectTransform.anchoredPosition = x)
            .AddTo(handles);
    }
    void StartLoop()
    {

    }
    public void Close()
    {
        LMotion.Create(text.color, Color.clear, fadeOutTime)
            .WithEase(fadeOutEase)
            .Bind(x => text.color = x)
            .AddTo(handles);
        LMotion.Create(rectTransform.anchoredPosition, rectTransform.anchoredPosition + fadeOutOffset, fadeOutTime)
            .WithEase(fadeOutEase)
            .Bind(x => rectTransform.anchoredPosition = x)
            .AddTo(handles);
    }
}
