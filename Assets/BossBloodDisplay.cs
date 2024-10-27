using LitMotion;
using LitMotion.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BossBloodDisplay : MonoBehaviour, IObserver<BossBloodManager>
{
    public ObservedBossBloodManager ObservedBossBloodManager;
    public Element displayElement;
    public Slider slider;
    public float textScrambleTime;
    public Ease textScrambleEase;
    public TMP_Text bloodLeftText;
    public TMP_Text bossNameText;
    BossBloodManager bloodManager => ObservedBossBloodManager.GetReference();
    public int OrderPriority => 0;

    protected virtual void OnEnable()
    {
        ObservedBossBloodManager.RegisterObserver(this);
    }
    protected virtual void OnDisable()
    {
        ObservedBossBloodManager.UnregisterObserver(this);
    }
    public void OnSetReference(BossBloodManager previousRef, BossBloodManager newRef)
    {
        if(previousRef != null && newRef == null)
        {
            displayElement.ChangeScreenState(Element.ScreenState.TransitionToOffScreen);
            bloodLeftText.text = $"DEAD";
            return;
        }
        if(previousRef != newRef)
        {
            displayElement.ChangeScreenState(Element.ScreenState.TransitionToOnScreen);
            LMotion.String.Create512Bytes("", newRef.bossName, textScrambleTime)
                .WithRichText()
                .WithScrambleChars(ScrambleMode.Lowercase)
                .WithEase(textScrambleEase)
                .BindToText(bossNameText);
        }
    }
    void Update()
    {
        if (bloodManager == null)
            return;
        bloodLeftText.text = $"Blood Left {Mathf.RoundToInt(bloodManager.blood)} mL";
        slider.maxValue = bloodManager.bloodMax;
        slider.value = bloodManager.blood;
    }
}