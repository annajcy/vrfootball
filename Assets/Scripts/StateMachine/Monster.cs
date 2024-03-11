using System;
using UnityEngine;
using UnityEngine.AI;


public class Monster : MonoBehaviour, IAIObject
{
    private NavMeshAgent navMeshAgent;

    private StateMachine stateMachine;

    private Transform targetTransform;

    private Vector3 nowObjPos;

    public Vector3 nowPos {
        get
        {
            nowObjPos = this.transform.position;
            nowObjPos.y = 0;
            return nowObjPos;
        }
    }

    public Vector3 targetPos
    {
        get
        {
            return targetTransform.position - Vector3.up * 0.5f;
        }
    }

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine(this);
        stateMachine.AddState<PatrolState>();
        stateMachine.AddState<ChaseState>();
        stateMachine.ChangeState<PatrolState>();
    }

    private void Update()
    {
        stateMachine.UpdateState();
    }

    public void Move(Vector3 pos)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(pos);
    }

    public void StopMove()
    {
        navMeshAgent.isStopped = true;
    }

    public void Attack()
    {
        print("ATK");
    }

    public void ChangeAction(EAction action)
    {

    }


}