using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;
public partial class Element : MonoBehaviour
{
    protected GameObject SelectedObject => Elements.ins.selectedObject;
    protected bool isSelected;
    protected RectTransform rectTransform;

    protected virtual void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        screenStateMachine.AddState(ScreenState.OffScreen, OffScreen_OnEnter, OffScreen_OnLogic);
        screenStateMachine.AddState(ScreenState.TransitionToOnScreen, TransitionToOnScreen_OnEnter, TransitionToOnScreen_OnLogic);
        screenStateMachine.AddState(ScreenState.OnScreen, OnScreen_OnEnter, OnScreen_OnLogic);
        screenStateMachine.AddState(ScreenState.TransitionToOffScreen, TransitionToOffScreen_OnEnter, TransitionToOffScreen_OnLogic);
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
}