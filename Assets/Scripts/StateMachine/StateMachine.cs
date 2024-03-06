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
    private Dictionary<EState, BaseState> states = new Dictionary<EState, BaseState>();
    private BaseState nowState;
    
    public void ChangeState(EState state)
    {
        nowState?.OnQuit();

        if (states.ContainsKey(state))
        {
            nowState = states[state];
            nowState.OnEnter();
        }
    }

    public void UpdateState()
    {
        nowState?.OnUpdate();
    }

}