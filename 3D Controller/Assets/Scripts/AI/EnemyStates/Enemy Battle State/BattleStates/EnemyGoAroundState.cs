using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGoAroundState : EnemyBaseState
{
    private NavMeshAgent NavMeshAgent;
    private Animator Animator;
    private EnemyDetectionScript EnemyDetection;
    public EnemyGoAroundState(EnemyBattleStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Animator _animator, EnemyDetectionScript _enemyDetectionScript) : base(_enemyStateMachine)
    {
        NavMeshAgent = _navMeshAgent;
        Animator = _animator;

        EnemyDetection = _enemyDetectionScript;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        //Set Destination within Battle Range, but not behind Player
        NavMeshAgent.SetDestination(Random.insideUnitSphere * EnemyDetection.BattleSphereRadius);
        NavMeshAgent.isStopped = false;
        Animator.SetBool("isWalking", true);
        Debug.Log("Enter Go Around State");


    }

    public override void StateUpdate()
    {
        //Move to Destination
        //play Move Animaiton
        Debug.Log("Goes Around");
    }

    public override void StateExit()
    {
        //Cancel Walk Animation and Movement
        NavMeshAgent.isStopped = true;
        Animator.SetBool("isWalking", false);
        Debug.Log("Exit Go Around State");

    }

}
