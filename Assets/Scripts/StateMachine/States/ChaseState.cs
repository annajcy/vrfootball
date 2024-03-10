using System;
using UnityEngine;

public class ChaseState : BaseState
{
    public ChaseState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override EState AIState => EState.Chase;

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