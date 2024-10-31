using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine.UI;
using uPools;
using UnityEngine.Events;
public class UpgradeSelection : MonoBehaviour, IPoolCallbackReceiver, IObserver<Upgrade>
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Button chooseUpgradeButton;
    //[TextArea] public string upgradeTextDebug;
    MotionHandle descriptionTextMotion;
    MotionHandle nameTextMotion;
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

    public UpgradePool upgradePool;

    public UnityEvent onRent;
    public ObservedUpgrade ObservedUpgradeChoice;
    public UnityEvent onUpgradeChoiceMade;
    public int OrderPriority => 0;
    public float returnToPoolTime;
    float returnToPoolTimer;
    void Awake()
    {
        chooseUpgradeButton.onClick.AddListener(ChooseUpgrade);
    }
    void OnEnable()
    {
        ObservedUpgradeChoice.RegisterObserver(this);
    }
    void OnDisable()
    {
        ObservedUpgradeChoice.UnregisterObserver(this);
    }
    void ChooseUpgrade()
    {
        upgrade.ActivateUpgrade();
        ObservedUpgradeChoice.SetReference(upgrade);
    }
    void UpgradeTextMotion()
    {
        descriptionTextMotion = LMotion.String.Create512Bytes("", upgrade.GetUpgradeDescription(), textTime)
            .WithRichText()
            .WithEase(textEase)
            .WithOnComplete(() => ButtonMotion(MotionType.Show))
            .BindToText(descriptionText);
        nameTextMotion = LMotion.String.Create512Bytes("", upgrade.upgradeName, textTime)
            .WithRichText()
            .WithEase(textEase)
            .BindToText(nameText);
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
        upgrade = upgradePool.GetRandomUpgrade();
        InitialSetup();
        UpgradeTextMotion();
        onRent?.Invoke();
    }
    public void OnReturn()
    {
        CancelMotions();
        transform.SetParent(null);
    }
    void CancelMotions()
    {
        if(nameTextMotion.IsActive())
            nameTextMotion.Cancel();
        if(descriptionTextMotion.IsActive())
            descriptionTextMotion.Cancel();
        if (buttonMotion.IsActive())
            buttonMotion.Cancel();
    }
    void InitialSetup()
    {
        chooseUpgradeButton.transform.localScale = new Vector3(0, 1, 1);
    }
    public void OnSetReference(Upgrade previousRef, Upgrade newRef)
    {
        if(newRef != null)
        {
            ButtonMotion(MotionType.Hide);
            onUpgradeChoiceMade?.Invoke();
            returnToPoolTimer = returnToPoolTime;
        }
    }
    void Update()
    {
        if(returnToPoolTimer > 0)
        {
            returnToPoolTimer -= Time.deltaTime;
            if(returnToPoolTimer <= 0)
            {
                SharedGameObjectPool.Return(gameObject);
            }
        }
    }
}
