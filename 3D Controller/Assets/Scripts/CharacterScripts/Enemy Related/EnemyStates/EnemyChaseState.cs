using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{

    private Transform PlayerPosition;
    private Transform EnemyTransform;
    

    public EnemyChaseState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMesh, Transform _playerPosition, Animator _animator, Transform enemyTransform, EnemyScript _enemyScript) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript)
    {
        PlayerPosition = _playerPosition;
        EnemyTransform = enemyTransform;

        velocityHashZ = Animator.StringToHash("VelocityZ");
    }


    public override void StateEnter()
    {
        navMesh.isStopped = false;
    }

    public override void StateUpdate()
    {
        EnemyTransform.LookAt(PlayerPosition);
        navMesh.SetDestination(PlayerPosition.position);

        velocityZ = Mathf.Clamp(velocityZ + Time.deltaTime * acceleration, velocityX, maxVelocity);

        animator.SetFloat(velocityHashZ, velocityZ);
    }

    public override void StateExit()
    {
        velocityZ = 0f;
        animator.SetFloat(velocityHashZ, velocityZ);
        navMesh.isStopped = true;
        Debug.Log(velocityZ);

    }

}
