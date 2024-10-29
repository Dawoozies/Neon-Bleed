using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine.UI;
using uPools;
public class UpgradeSelection : MonoBehaviour, IPoolCallbackReceiver
{
    public TMP_Text upgradeText;
    public Button chooseUpgradeButton;
    [TextArea] public string upgradeTextDebug;
    MotionHandle textMotion;
    MotionHandle buttonMotion;
    public Upgrade upgrade;
    enum MotionType
    {
        Show, Hide
    }
    public float textTime;
    public float buttonTime;
    public Ease textEase;
    public Ease buttonEase;
    void Awake()
    {
        chooseUpgradeButton.onClick.AddListener(ChooseUpgrade);
        UpgradeTextMotion(upgradeTextDebug);
        InitialSetup();
    }
    void ChooseUpgrade()
    {
        upgrade.ActivateUpgrade();
        ButtonMotion(MotionType.Hide);
    }
    void UpgradeTextMotion(string text)
    {
        if (textMotion.IsActive())
        {
            textMotion.Cancel();
        }
        upgradeText.color = Color.white;
        textMotion = LMotion.String.Create512Bytes("", text, textTime)
            .WithRichText()
            .WithEase(textEase)
            .WithOnComplete(() => ButtonMotion(MotionType.Show))
            .BindToText(upgradeText);
    }
    void ButtonMotion(MotionType motionType)
    {
        Vector3 targetScale = new Vector3(0,1,1);
        switch (motionType)
        {
            case MotionType.Show:
                targetScale = Vector3.one;
                break;
        }
        buttonMotion = LMotion.Create(chooseUpgradeButton.transform.localScale, targetScale, buttonTime)
            .WithEase(buttonEase)
            .Bind(x => chooseUpgradeButton.transform.localScale = x);
    }
    void OnDestroy()
    {
        CancelMotions();
    }
    public void OnRent()
    {
        InitialSetup();
    }
    public void OnReturn()
    {
        CancelMotions();
    }
    void CancelMotions()
    {
        if(textMotion.IsActive())
            textMotion.Cancel();
        if (buttonMotion.IsActive())
            buttonMotion.Cancel();
    }
    void InitialSetup()
    {
        chooseUpgradeButton.transform.localScale = new Vector3(0, 1, 1);
    }
}
