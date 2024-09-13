using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;
public class Element : MonoBehaviour, IObserver<GameObject>
{
    protected bool isSelected;
    protected RectTransform rectTransform;
    protected Menu encapsulatingMenu;
    protected bool inMenu;
    public enum ScreenState
    {
        OffScreen,
        TransitionToOnScreen,
        OnScreen,
        TransitionToOffScreen,
    }

    [Disable] public ScreenState screenState;
    public ScreenState startingScreenState;
    public OffScreenSide offScreenSide;
    protected Vector3 onScreenPosition;
    public float onScreenTransitionTime;
    public Ease onScreenTransitionEasing;
    protected StateMachine<ScreenState> screenStateMachine;
    protected MotionHandle transitionMotionHandle;
    public bool hasDelay;
    public ObservedGameObject ObservedSelectedObject;

    public int OrderPriority => orderPriority;
    public int orderPriority;
    protected virtual void OnEnable()
    {
        ObservedSelectedObject.RegisterObserver(this);
    }
    protected virtual void OnDisable()
    {
        ObservedSelectedObject.UnregsiterObserver(this);
    }
    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //Elements.ins.RegisterSelectedObjectChangeCallback(OnSelectedObjectChanged, 1);
        //intead of register selected object callback

        encapsulatingMenu = GetComponentInParent<Menu>();
        if (encapsulatingMenu != null)
            inMenu = true;
        onScreenPosition = rectTransform.localPosition;
        rectTransform.position = StaticData.ins.OffScreenPoint(rectTransform.position, offScreenSide);
        screenStateMachine = new StateMachine<ScreenState>();
        screenStateMachine.AddState(ScreenState.OffScreen, OffScreen_OnEnter, OffScreen_OnLogic);
        screenStateMachine.AddState(ScreenState.TransitionToOnScreen, TransitionToOnScreen_OnEnter, TransitionToOnScreen_OnLogic);
        screenStateMachine.AddState(ScreenState.OnScreen, OnScreen_OnEnter, OnScreen_OnLogic);
        screenStateMachine.AddState(ScreenState.TransitionToOffScreen, TransitionToOffScreen_OnEnter, TransitionToOffScreen_OnLogic);
        screenStateMachine.SetStartState(startingScreenState);
        screenStateMachine.Init();
    }
    protected virtual void OnDestroy()
    {
        //Elements.ins.RemoveSelectedObjectChangeCallback(OnSelectedObjectChanged, 1);
    }
    protected virtual void Update()
    {
        screenState = screenStateMachine.ActiveStateName;
    }
    protected virtual void OnAspectRatioChanged()
    {
    }
    public virtual void OnSetReference(GameObject prevSelectedObject, GameObject selectedObject)
    {
        if(gameObject == selectedObject)
        {
            if (!isSelected)
            {
                OnMouseOverEnter();
                isSelected = true;
            }
            else
            {
                OnMouseOverUpdate();
            }
        }
        if(gameObject != selectedObject)
        {
            if(isSelected)
            {
                OnMouseOverExit();
                isSelected = false;
            }
        }
    }
    protected virtual void OnMouseOverEnter()
    {
        Debug.Log($"name = {gameObject.name} OnMouseOverEnter");
    }
    protected virtual void OnMouseOverUpdate()
    {
        Debug.Log($"name = {gameObject.name} OnMouseOverUpdate");
    }
    protected virtual void OnMouseOverExit()
    {
        Debug.Log($"name = {gameObject.name} OnMouseOverExit");
    }
    protected virtual void OffScreen_OnEnter(StateBase<ScreenState> state)
    {
    }
    protected virtual void OffScreen_OnLogic(StateBase<ScreenState> state)
    {
    }
    protected virtual void TransitionToOnScreen_OnEnter(StateBase<ScreenState> state)
    {
        //start transition
        if(!hasDelay)
        {
            transitionMotionHandle = LMotion.Create(StaticData.ins.OffScreenPoint(rectTransform.localPosition, offScreenSide), onScreenPosition, onScreenTransitionTime)
                .WithEase(onScreenTransitionEasing)
                .Bind(x => rectTransform.localPosition = x);
        }
        else
        {
            transitionMotionHandle = LMotion.Create(StaticData.ins.OffScreenPoint(rectTransform.localPosition, offScreenSide), onScreenPosition, onScreenTransitionTime)
                .WithEase(onScreenTransitionEasing)
                .WithDelay(StaticData.ins.GetDelay(rectTransform.localPosition, offScreenSide))
                .Bind(x => rectTransform.localPosition = x);
        }
    }
    protected virtual void TransitionToOnScreen_OnLogic(StateBase<ScreenState> state)
    {
        //update transition
        if(!transitionMotionHandle.IsActive())
        {
            screenStateMachine.RequestStateChange(ScreenState.OnScreen);
        }
    }
    protected virtual void OnScreen_OnEnter(StateBase<ScreenState> state)
    {
    }
    protected virtual void OnScreen_OnLogic(StateBase<ScreenState> state)
    {
    }
    protected virtual void TransitionToOffScreen_OnEnter(StateBase<ScreenState> state)
    {
    }
    protected virtual void TransitionToOffScreen_OnLogic(StateBase<ScreenState> state)
    {
    }
}