using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPools;

public class AngelColorEffect : MonoBehaviour, IPoolCallbackReceiver
{
    public Gradient angelGradient;
    float gradientTime;
    public float gradientTimeMax;
    SpriteRenderer spriteRenderer;
    bool dead;
    public float deathMultiplier;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(dead)
        {
            return;
        }

        gradientTime += Time.deltaTime;
        if(gradientTime > gradientTimeMax)
        {
            gradientTime = 0f;
        }

        spriteRenderer.color = angelGradient.Evaluate(gradientTime/gradientTimeMax);
    }
    public void KillMe()
    {
        if(!dead)
        {
            dead = true;
            spriteRenderer.color *= deathMultiplier;
        }
    }

    public void OnRent()
    {
    }
    public void OnReturn()
    {
        dead = false;
    }
}
