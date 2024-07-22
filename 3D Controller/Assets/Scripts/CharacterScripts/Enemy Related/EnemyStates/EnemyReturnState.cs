using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReturnState : EnemyBaseState
{
    private Vector3 StartPosition;
    public EnemyReturnState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMesh, Vector3 _startPosition, Animator _animator) : base(_enemyStateMachine, _animator, _navMesh)
    {
        StartPosition = _startPosition;
    }


    public override void StateEnter()
    {
        //Debug.Log("OnReturnEnter");
    }

    public override void StateUpdate()
    {
        navMesh.SetDestination(StartPosition);
        animator.SetBool("isWalking", true);
    }

    public override void StateExit()
    {
        animator.SetBool("isWalking", false);
    }

}
