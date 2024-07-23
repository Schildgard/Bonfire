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
        velocityHashZ = Animator.StringToHash("VelocityZ");
    }


    public override void StateEnter()
    {
        //Debug.Log("OnReturnEnter");
        navMesh.isStopped = false;
    }

    public override void StateUpdate()
    {
        navMesh.SetDestination(StartPosition);
        velocityZ = Mathf.Clamp(velocityZ + Time.deltaTime * acceleration, velocityX, maxVelocity);

        animator.SetFloat(velocityHashZ, velocityZ);
    }

    public override void StateExit()
    {
        velocityZ = 0f;
        navMesh.isStopped=true;
    }

}
