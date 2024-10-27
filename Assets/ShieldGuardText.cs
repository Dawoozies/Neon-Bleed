using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LitMotion;
public class ShieldGuardText : MonoBehaviour, IObserver<int>
{
    public ObservedInt ObservedPlayerBloodShield;
    TMP_Text text;
    public int OrderPriority => 0;
    public float shakeRadius;
    public float shakeTime;
    float shakeTimer;
    public Gradient exposedGradient;
    ColorPalette ActiveColorPalette => StaticData.ins.ActiveColorPalette;
    public int guardedColorPaletteIndex;
    Vector3 originPos;
    bool exposed;
    void OnEnable()
    {
        ObservedPlayerBloodShield.RegisterObserver(this);
    }
    void OnDisable()
    {
        ObservedPlayerBloodShield.UnregisterObserver(this);
    }
    public void OnSetReference(int previousRef, int newRef)
    {
        if(previousRef > 0 && newRef <= 0)
        {
            text.text = "EXPOSED!!!";
            text.color = exposedGradient.Evaluate(0f);
            exposed = true;
        }
        if(newRef >= 1)
        {
            text.text = "GUARDED";
            text.color = ActiveColorPalette.colors[guardedColorPaletteIndex];
            transform.localPosition = originPos;
            exposed = false;
        }
    }

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        originPos = transform.localPosition;
    }
    void Update()
    {
        if (!exposed)
            return;

        if (shakeTimer < shakeTime)
        {
            shakeTimer += Time.deltaTime;
        }
        else
        {
            transform.localPosition = originPos + (Vector3)Random.insideUnitCircle * shakeRadius;
            shakeTimer = 0f;
        }

        text.color = exposedGradient.Evaluate(shakeTimer/shakeTime);
    }
}
