using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletGraphic : MonoBehaviour
{
    public Vector3 sizeA;
    public Vector3 sizeB;
    public Vector3 size;
    SpriteRenderer spriteRenderer;
    public Gradient colorGradient;
    public AnimationCurve lerpCurve;
    public float time;
    float t;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        size = Vector3.Lerp(sizeA, sizeB, lerpCurve.Evaluate(t/time));
        spriteRenderer.color = colorGradient.Evaluate(t/time);

        transform.localScale = size;

        if(t > time)
        {
            t = 0f;
        }
        else
        {
            t += Time.deltaTime;
        }
    }
}