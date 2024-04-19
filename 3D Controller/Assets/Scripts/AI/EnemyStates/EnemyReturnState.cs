using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReturnState : EnemyBaseState
{
    private Vector3 StartPosition;
    private NavMeshAgent NavMeshAgent;
    public EnemyReturnState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Vector3 _startPosition) : base(_enemyStateMachine)
    {
        StartPosition = _startPosition;
        NavMeshAgent = _navMeshAgent;
    }


    public override void StateEnter()
    {
        Debug.Log("OnReturnEnter");
    }

    public override void StateUpdate()
    {
        NavMeshAgent.SetDestination(StartPosition);
        Debug.Log("OnReturnUpdate");
    }

    public override void StateExit()
    {
        Debug.Log("OnReturnExit");
    }

}
