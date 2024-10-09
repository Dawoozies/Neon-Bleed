using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCursor : MonoBehaviour
{
    public float rotationSpeedMax;
    public float baseRotation;
    public AnimationCurve shootSpeedCurve;
    public float mouseHeldTime;
    public float mouseHeldTimeMax;
    float angle;
    public Gradient colorGradient;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            mouseHeldTime += Time.deltaTime;
        }
        else
        {
            mouseHeldTime = 0f;
        }
        angle = (baseRotation + rotationSpeedMax * shootSpeedCurve.Evaluate(mouseHeldTime / mouseHeldTimeMax)) * Time.deltaTime;
        spriteRenderer.color = colorGradient.Evaluate(mouseHeldTime/mouseHeldTimeMax);
        transform.Rotate(Vector3.forward, angle);
        transform.position = MousePosition.ins.WorldPos;
    }
}
