using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public StateMachine() { }
    private Dictionary<string, BaseState> stateDict = new Dictionary<string, BaseState>();
    private string nowStateName;

    private BaseState nowState()
    {
        if (nowStateName == null) return null;
        return stateDict[nowStateName];
    }

    public void AddState<T>() where T : BaseState, new()
    {
        string name = typeof(T).Name;
        var state = new T();
        state.Init(this);
        stateDict.Add(name, state);
    }
    
    public void ChangeState<T>()
    {
        string name = typeof(T).Name;
        nowState()?.OnQuit();

        if (stateDict.ContainsKey(name))
        {
            nowStateName = name;
            nowState()?.OnEnter();
        }
    }

    public void UpdateState()
    {
        nowState()?.OnUpdate();
    }

}