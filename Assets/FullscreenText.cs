using LitMotion;
using LitMotion.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FullscreenText : MonoBehaviour
{
    public static FullscreenText ins;
    private void Awake()
    {
        ins = this;
    }
    public string playOnStart;
    MotionHandle textMotion;
    TMP_Text text;
    public float textScrambleTime;
    public Ease textScrambleEase;
    public float textFadeTime;
    public Ease fadeEase;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
        DisplayFullscreenText(playOnStart);
    }
    public void DisplayFullscreenText(string toDisplay)
    {
        if(textMotion.IsActive())
        {
            textMotion.Cancel();
        }
        text.color = Color.white;
        textMotion = LMotion.String.Create512Bytes("", toDisplay, textScrambleTime)
            .WithRichText()
            .WithScrambleChars(ScrambleMode.Lowercase)
            .WithEase(textScrambleEase)
            .WithOnComplete(FadeText)
            .BindToText(text);
    }
    public void FadeText()
    {
        LMotion.Create(text.color, Color.clear, textFadeTime)
            .WithEase(fadeEase)
            .Bind(x => text.color = x);
    }
}
