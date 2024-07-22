using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyGoAroundState : EnemyBaseState
{
    private EnemyDetectionScript EnemyDetection;
    private Transform Player;

    private Vector3 Destination;


    public EnemyGoAroundState(EnemyBattleStateMachine _enemyStateMachine, NavMeshAgent _navMesh, Animator _animator, EnemyDetectionScript _enemyDetectionScript, Transform _player) : base(_enemyStateMachine, _animator, _navMesh)
    {
        Player = _player;
        EnemyDetection = _enemyDetectionScript;


    }

    public override void StateEnter()
    {
        base.StateEnter();
        Destination = BattleStateMachine.EnemyPosition.position + new Vector3(BattleStateMachine.EnemyPosition.right.x *Random.Range(-2, 3), 0, BattleStateMachine.EnemyPosition.forward.z * Random.Range(0, 2));
        BattleStateMachine.TargetPosition = Destination;
        

        //Set Destination within Battle Range, but not behind Player
        navMesh.SetDestination(Destination);
        navMesh.isStopped = false;
        //Animator.SetBool("isWalking", true);



    }

    public override void StateUpdate()
    {

        Vector3 DistanceToDestination = Destination - BattleStateMachine.EnemyPosition.position;

        if (DistanceToDestination.x > 0f + distanceTolerance) // Comparison of float Vector3! Exchange with Distance formular!!
        {
            //Increase BlendTree Velocity X
            velocityX = Mathf.Clamp(velocityX + Time.deltaTime * acceleration, velocityX, maxVelocity);
        }
        else if (DistanceToDestination.x < 0f - distanceTolerance) 
        {
            //Decrease BlendTree Velocity X
            velocityX = Mathf.Clamp(velocityX - Time.deltaTime * acceleration, -maxVelocity, velocityX);
        }
        else
        {
            velocityX = velocityX = 0f;
        }
        if (DistanceToDestination.y > 0f + distanceTolerance)
        {
            //Increase BlendTree Velocity Y
            velocityZ = Mathf.Clamp(velocityZ + Time.deltaTime * acceleration, velocityZ, maxVelocity);
        }
        else if (DistanceToDestination.y < 0f - distanceTolerance)
        {
            //Decrease BlendTree Velocity Y
            velocityZ = Mathf.Clamp(velocityZ - Time.deltaTime * acceleration, -maxVelocity, velocityZ);
        }
        else
        {
            velocityZ = velocityZ = 0f;
        }

        animator.SetFloat(velocityHashX, velocityX);
        animator.SetFloat(velocityHashZ, velocityZ);


    }

    public override void StateExit()
    {
        navMesh.isStopped = true;
        animator.SetBool("isWalking", false);
    }

}
