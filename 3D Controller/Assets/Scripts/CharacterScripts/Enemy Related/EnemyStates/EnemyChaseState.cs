using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{

    private Transform PlayerPosition;
    private Transform EnemyTransform;

    public EnemyChaseState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMesh, Transform _playerPosition, Animator _animator, Transform enemyTransform) : base(_enemyStateMachine, _animator, _navMesh)
    {
        PlayerPosition = _playerPosition;
        EnemyTransform = enemyTransform;

        velocityHashZ = Animator.StringToHash("VelocityZ");
    }


    public override void StateEnter()
    {
       Debug.Log("Enter Chase State");
    }

    public override void StateUpdate()
    {
        EnemyTransform.LookAt(PlayerPosition);
        navMesh.SetDestination(PlayerPosition.position);
        navMesh.isStopped = false;

        animator.SetFloat(velocityHashZ, velocityZ);

        velocityZ = Mathf.Clamp(velocityZ + Time.deltaTime * acceleration, velocityX, maxVelocity);
        // Animator.SetBool("isWalking", true);
    }

    public override void StateExit()
    {
        velocityZ = 0f;
        animator.SetFloat(velocityHashZ, velocityZ);
        //Animator.SetBool("isWalking", false);
    }

}
