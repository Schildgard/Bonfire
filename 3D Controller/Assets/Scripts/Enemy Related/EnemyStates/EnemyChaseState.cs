using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{

    private NavMeshAgent NavMeshAgent;
    private Transform PlayerPosition;
    private Animator Animator;
    private Transform EnemyTransform;

    public EnemyChaseState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Transform _playerPosition, Animator animator, Transform enemyTransform) : base(_enemyStateMachine)
    {
        NavMeshAgent = _navMeshAgent;
        PlayerPosition = _playerPosition;
        Animator = animator;
        EnemyTransform = enemyTransform;
    }


    public override void StateEnter()
    {
    }

    public override void StateUpdate()
    {
        EnemyTransform.LookAt(PlayerPosition);
        NavMeshAgent.SetDestination(PlayerPosition.position);
        NavMeshAgent.isStopped = false;
        Animator.SetBool("isWalking", true);
    }

    public override void StateExit()
    {
        Animator.SetBool("isWalking", false);
    }
}
