using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{

    private Transform PlayerPosition;
    private Transform enemyTransform;

    private EnemyDetectionScript detection;
    

    public EnemyChaseState(EnemyStateMachineBase _enemyStateMachine, NavMeshAgent _navMesh, Transform _playerPosition, Animator _animator, Transform _enemyTransform,EnemyScript _enemyScript, EnemyDetectionScript _detection) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript)
    {
        PlayerPosition = _playerPosition;
        enemyTransform = _enemyTransform;
        detection = _detection;

        velocityHashZ = Animator.StringToHash("VelocityZ");
    }


    public override void StateEnter()
    {
        navMesh.isStopped = false;
        detection.Detected = true;
    }

    public override void StateUpdate()
    {
        enemyTransform.LookAt(PlayerPosition);
        navMesh.SetDestination(PlayerPosition.position);

        velocityZ = Mathf.Clamp(velocityZ + Time.deltaTime * acceleration, velocityX, maxVelocity);

        animator.SetFloat(velocityHashZ, velocityZ);
    }

    public override void StateExit()
    {
        velocityZ = 0f;
        animator.SetFloat(velocityHashZ, velocityZ);
        navMesh.isStopped = true;
    }

}
