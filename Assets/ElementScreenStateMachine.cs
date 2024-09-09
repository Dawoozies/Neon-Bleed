using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;
public partial class Element : MonoBehaviour
{
    public enum ScreenState
    {
        OffScreen,
        TransitionToOnScreen,
        OnScreen,
        TransitionToOffScreen,
    }
    public ScreenState screenState;
    protected StateMachine<ScreenState> screenStateMachine;
    protected virtual void OffScreen_OnEnter(StateBase<ScreenState> state)
    {
    }
    protected virtual void OffScreen_OnLogic(StateBase<ScreenState> state)
    {
    }
    protected virtual void TransitionToOnScreen_OnEnter(StateBase<ScreenState> state)
    {
        //start transition
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
