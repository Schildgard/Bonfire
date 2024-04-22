using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{

    private NavMeshAgent NavMeshAgent;
    private Transform PlayerPosition;
    private Animator Animator;

    public EnemyChaseState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Transform _playerPosition, Animator animator) : base(_enemyStateMachine)
    {
        NavMeshAgent = _navMeshAgent;
        PlayerPosition = _playerPosition;
        Animator = animator;
    }


    public override void StateEnter()
    {
        //Debug.Log("OnChaseEnter");
    }

    public override void StateUpdate()
    {
        NavMeshAgent.SetDestination(PlayerPosition.position);
        Animator.SetBool("isWalking", true);
       // Debug.Log("OnChaseUpdate");
    }

    public override void StateExit()
    {
        Animator.SetBool("isWalking", false);
        // Debug.Log("OnChaseExit");
    }
}
