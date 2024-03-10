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

    }

    public override void OnQuit()
    {

    }

    public override void OnUpdate()
    {

    }
}