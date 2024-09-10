using LitMotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;
public class Element : MonoBehaviour
{
    protected GameObject SelectedObject => Elements.ins.selectedObject;
    protected bool isSelected;
    protected RectTransform rectTransform;
    public enum ScreenState
    {
        OffScreen,
        TransitionToOnScreen,
        OnScreen,
        TransitionToOffScreen,
    }
    public ScreenState screenState;
    public OffScreenSide offScreenSide;
    protected Vector3 onScreenPosition;
    public float onScreenTransitionTime;
    public Ease onScreenTransitionEasing;
    protected StateMachine<ScreenState> screenStateMachine;
    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        onScreenPosition = rectTransform.position;
        rectTransform.position = StaticData.ins.OffScreenPoint(rectTransform.position, offScreenSide);
        screenStateMachine = new StateMachine<ScreenState>();
        screenStateMachine.AddState(ScreenState.OffScreen, OffScreen_OnEnter, OffScreen_OnLogic);
        screenStateMachine.AddState(ScreenState.TransitionToOnScreen, TransitionToOnScreen_OnEnter, TransitionToOnScreen_OnLogic);
        screenStateMachine.AddState(ScreenState.OnScreen, OnScreen_OnEnter, OnScreen_OnLogic);
        screenStateMachine.AddState(ScreenState.TransitionToOffScreen, TransitionToOffScreen_OnEnter, TransitionToOffScreen_OnLogic);
        screenStateMachine.SetStartState(screenState);
        screenStateMachine.Init();
    }
    protected virtual void Update()
    {
        if(gameObject == SelectedObject)
        {
            if(!isSelected)
            {
                //first frame this has been selected
                OnMouseOverEnter();
                isSelected = true;
            }
            else
            {
                OnMouseOverUpdate();
            }
        }

        if(gameObject != SelectedObject)
        {
            if(isSelected)
            {
                //first frame this has been not selected
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
        LMotion.Create(StaticData.ins.OffScreenPoint(rectTransform.position, offScreenSide), onScreenPosition, onScreenTransitionTime)
            .WithEase(onScreenTransitionEasing)
            .WithDelay(StaticData.ins.GetDelay(rectTransform.position, offScreenSide))
            .Bind(x => rectTransform.position = x);
    }
    protected virtual void TransitionToOnScreen_OnLogic(StateBase<ScreenState> state)
    {
        //update transition
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