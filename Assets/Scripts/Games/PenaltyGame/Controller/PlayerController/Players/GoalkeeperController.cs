using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoalkeeperController : PlayerController
{
    private void OnEnable()
    {
        Respawn();
        SwitchPose(GoalkeeperAction.Ready);
    }

    public GoalkeeperAction GetPredictedAction(Vector3 speed)
    {
        animator.speed = Random.Range(1.0f, 1.5f);
        return (GoalkeeperAction)Random.Range(1, 9);
    }

    public void SwitchPose(GoalkeeperAction goalkeeperAction)
    {
        animator.SetInteger(state, (int)goalkeeperAction);
    }

    public void React(Vector3 speed)
    {
        SwitchPose(GetPredictedAction(speed));
    }
}