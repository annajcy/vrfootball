using System;
using UnityEngine;

public abstract class BaseState
{
    protected StateMachine stateMachine;
    public BaseState() { }

    public virtual void Init(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnQuit();

}