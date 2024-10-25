using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class BloodColor : MonoBehaviour, IPoolCallbackReceiver
{
    public Gradient bloodGradient;
    public Color rotColor;
    public float rotAmount;
    public float gradientTimeMax;
    float gradientTime;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnRent()
    {
        rotAmount = 0f;
    }
    public void OnReturn()
    {
    }
    void Update()
    {
        gradientTime += Time.deltaTime;
        if (gradientTime > gradientTimeMax)
        {
            gradientTime = 0f;
        }
        spriteRenderer.color = Color.Lerp(bloodGradient.Evaluate(gradientTime / gradientTimeMax), rotColor, rotAmount);
    }
    public void RotProgressUpdate(float rotAmount)
    {
        this.rotAmount = rotAmount;
    }
}
