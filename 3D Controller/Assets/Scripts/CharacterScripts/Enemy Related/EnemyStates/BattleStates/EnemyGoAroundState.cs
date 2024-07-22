using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyGoAroundState : EnemyBaseState
{
    private NavMeshAgent NavMeshAgent;
    private Animator Animator;
    private EnemyDetectionScript EnemyDetection;
    private Transform Player;

    private Vector3 Destination;

    private float velocityX;
    private float velocityZ;

    private int VelocityHashX;
    private int VelocityHashZ;

    private float acceleration = 5f;
    private float maxVelocity = 0.5f;
    public EnemyGoAroundState(EnemyBattleStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Animator _animator, EnemyDetectionScript _enemyDetectionScript, Transform _player) : base(_enemyStateMachine)
    {
        NavMeshAgent = _navMeshAgent;
        Animator = _animator;
        Player = _player;
        EnemyDetection = _enemyDetectionScript;

        Animator.SetFloat(VelocityHashX, velocityX);
        Animator.SetFloat(VelocityHashZ, velocityZ);
    }

    public override void StateEnter()
    {
        base.StateEnter();
        Destination = BattleStateMachine.EnemyPosition.position + new Vector3(BattleStateMachine.EnemyPosition.right.x *Random.Range(-2, 3), 0, BattleStateMachine.EnemyPosition.forward.z * Random.Range(0, 2));
        BattleStateMachine.TargetPosition = Destination;
        

        //Set Destination within Battle Range, but not behind Player
        NavMeshAgent.SetDestination(Destination);
        NavMeshAgent.isStopped = false;
        Animator.SetBool("isWalking", true);



    }

    public override void StateUpdate()
    {
        if (Destination.x > BattleStateMachine.EnemyPosition.position.x) // Comparison of float Vector3! Exchange with Distance formular!!
        {
            //Increase BlendTree Velocity X
            velocityX = Mathf.Clamp(velocityX + Time.deltaTime * acceleration, velocityX, maxVelocity);
        }
        if (Destination.x < BattleStateMachine.EnemyPosition.position.x) 
        {
            //Decrease BlendTree Velocity X
            velocityX = Mathf.Clamp(velocityX - Time.deltaTime * acceleration, -maxVelocity, velocityX);
        }
        if (Destination.y > BattleStateMachine.EnemyPosition.position.y)
        {
            //Increase BlendTree Velocity Y
            velocityZ = Mathf.Clamp(velocityZ + Time.deltaTime * acceleration, velocityZ, maxVelocity);
        }
        if (Destination.y < BattleStateMachine.EnemyPosition.position.y)
        {
            //Decrease BlendTree Velocity Y
            velocityZ = Mathf.Clamp(velocityZ - Time.deltaTime * acceleration, -maxVelocity, velocityZ);
        }



    }

    public override void StateExit()
    {
        NavMeshAgent.isStopped = true;
        Animator.SetBool("isWalking", false);
    }

}
