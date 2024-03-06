using System;
using UnityEngine;

public class RunState : BaseState
{
    public RunState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override EState AIState => EState.Run;

    public override void OnEnter()
    {
        throw new NotImplementedException();
    }

    public override void OnQuit()
    {
        throw new NotImplementedException();
    }

    public override void OnUpdate()
    {
        throw new NotImplementedException();
    }
}