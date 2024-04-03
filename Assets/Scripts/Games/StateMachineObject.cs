using System;
using UnityEngine;

public class StateMachineObject<T> : SingletonMonoObject<T> where T : MonoBehaviour
{
    protected StateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        InitStateMachine();
    }

    protected virtual void Update()
    {
        stateMachine.UpdateState();
    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine();
        AddStates();
    }

    public StateMachine GetStateMachine()
    {
        return stateMachine;
    }

    protected virtual void AddStates() { }
}