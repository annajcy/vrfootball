using System;
using UnityEngine;

public abstract class BaseState
{
    public BaseState() { }

    public BaseState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void Init(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
    protected StateMachine stateMachine;

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnQuit();

}