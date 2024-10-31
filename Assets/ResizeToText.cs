using LitMotion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ResizeToText : MonoBehaviour
{
    public TMP_Text text;
    public Vector2 textSizeBounds;
    public Vector2 padding;
    RectTransform rectTransform;
    public float smoothingTime;
    Vector2 v;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //
        textSizeBounds = text.bounds.size;

        Vector2 size = Vector2.SmoothDamp(rectTransform.rect.size, textSizeBounds+padding, ref v, smoothingTime);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
    }
}
