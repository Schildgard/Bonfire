using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{

    private NavMeshAgent NavMeshAgent;
    private Transform PlayerPosition;

    public EnemyChaseState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Transform _playerPosition) : base(_enemyStateMachine)
    {
        NavMeshAgent = _navMeshAgent;
        PlayerPosition = _playerPosition;
    }


    public override void StateEnter()
    {
        Debug.Log("OnChaseEnter");
    }

    public override void StateUpdate()
    {
        NavMeshAgent.SetDestination(PlayerPosition.position);
        Debug.Log("OnChaseUpdate");
    }

    public override void StateExit()
    {
        Debug.Log("OnChaseExit");
    }
}
