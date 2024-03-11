using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public StateMachine(IAIObject aiObject)
    {
        this.aiObject = aiObject;
    }

    public IAIObject aiObject;
    private Dictionary<string, BaseState> states = new Dictionary<string, BaseState>();
    private string nowStateName;
    public BaseState nowState => states[nowStateName];

    public void AddState<T>() where T : BaseState, new()
    {
        string name = typeof(T).Name;
        var state = new T();
        state.Init(this);
        states.Add(name, state);
    }
    
    public void ChangeState<T>()
    {
        string name = typeof(T).Name;
        nowState?.OnQuit();

        if (states.ContainsKey(name))
        {
            nowStateName = name;
            nowState?.OnEnter();
        }
    }

    public void UpdateState()
    {
        nowState?.OnUpdate();
    }

}