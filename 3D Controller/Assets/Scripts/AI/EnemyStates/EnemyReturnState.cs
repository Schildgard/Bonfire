using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReturnState : EnemyBaseState
{
    private Vector3 StartPosition;
    private NavMeshAgent NavMeshAgent;
    private Animator Animator;
    public EnemyReturnState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Vector3 _startPosition, Animator animator) : base(_enemyStateMachine)
    {
        StartPosition = _startPosition;
        NavMeshAgent = _navMeshAgent;
        Animator = animator;
    }


    public override void StateEnter()
    {
        Debug.Log("OnReturnEnter");
    }

    public override void StateUpdate()
    {
        NavMeshAgent.SetDestination(StartPosition);
        Animator.SetBool("isWalking", true);
    }

    public override void StateExit()
    {
        Animator.SetBool("isWalking", false);
    }

}
