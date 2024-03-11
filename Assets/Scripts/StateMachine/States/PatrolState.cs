using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolState : BaseState
{
    private Vector3 targetPos;
    private int type;

    private EAction actionType;

    private Vector3 centerPos;
    private float radius;
    private float checkDis;

    private List<Vector3> pointList = new List<Vector3>();
    private int pointIdx;

    private int moveType;

    private bool isChangePos;
    private bool isAdd = true;

    private float waitTime = 2;

    public PatrolState(StateMachine stateMachine) : base(stateMachine)
    {
        type = 1;
        actionType = EAction.Sleep;

        centerPos = Vector3.zero;
        radius = 5;

        pointList.Add(new Vector3(1, 0, 0));
        pointList.Add(new Vector3(1, 0, 1));
        pointList.Add(new Vector3(0, 0, 1));
        pointList.Add(new Vector3(0, 0, 0));

        pointIdx = 2;
        moveType = 1;

        isChangePos = true;
    }

    public PatrolState()
    {

    }

    public override EState AIState => EState.Patrol;

    public override void OnEnter()
    {

    }

    public override void OnQuit()
    {
        stateMachine.aiObject.StopMove();
    }

    public override void OnUpdate()
    {
        if (type == 1)
        {
            stateMachine.aiObject.ChangeAction(actionType);
        }
        else if (type == 2)
        {
            if (isChangePos)
            {
                targetPos = Quaternion.Euler(0, Random.Range(0, 359f), 0) * Vector3.forward * radius + centerPos;
                isChangePos = false;
            }

            stateMachine.aiObject.Move(targetPos);

            if (Vector3.Distance(targetPos, stateMachine.aiObject.nowPos) < 0.2f)
                isChangePos = true;
        }
        else
        {
            if (isChangePos)
            {
                targetPos = pointList[pointIdx];
                if (moveType == 1)
                {
                    pointIdx++;
                    if (pointIdx == pointList.Count) pointIdx = 0;
                }
                else
                {
                    pointIdx = isAdd ? pointIdx + 1 : pointIdx - 1;
                    if (pointIdx == pointList.Count)
                    {
                        isAdd = false;
                        -- pointIdx;
                    }
                    else if (pointIdx < 0)
                    {
                        isAdd = true;
                        pointIdx++;
                    }

                    isChangePos = false;
                }
            }

            stateMachine.aiObject.Move(targetPos);

            if (Vector3.Distance(targetPos, stateMachine.aiObject.nowPos) < 0.2f)
                isChangePos = true;
        }


        if (Vector3.Distance(stateMachine.aiObject.nowPos, stateMachine.aiObject.targetPos) < checkDis)
        {
            stateMachine.ChangeState<ChaseState>();
        }

    }
}