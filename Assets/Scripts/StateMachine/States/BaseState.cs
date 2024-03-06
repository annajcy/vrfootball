using System;
using UnityEngine;

abstract public class BaseState
{
    public BaseState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
    protected StateMachine stateMachine;
    public virtual EState AIState { get; }
    public abstract void OnQuit();
    public abstract void OnUpdate();
    public abstract void OnEnter();

}