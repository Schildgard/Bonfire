using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReturnState : EnemyBaseState
{
    private Vector3 StartPosition;
    private EnemyDetectionScript detection;
    public EnemyReturnState(EnemyStateMachineBase _enemyStateMachine, NavMeshAgent _navMesh, Vector3 _startPosition, Animator _animator, EnemyScript _enemyScript, EnemyDetectionScript _detection) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript)
    {
        StartPosition = _startPosition;
        detection = _detection;

        velocityHashZ = Animator.StringToHash("VelocityZ");
    }


    public override void StateEnter()
    {
        navMesh.isStopped = false;
        detection.Detected = false;
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
        animator.SetFloat(velocityHashZ, velocityZ);
        navMesh.isStopped=true;
    }

}
