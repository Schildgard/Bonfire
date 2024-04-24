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
    public EnemyGoAroundState(EnemyBattleStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Animator _animator, EnemyDetectionScript _enemyDetectionScript, Transform _player) : base(_enemyStateMachine)
    {
        NavMeshAgent = _navMeshAgent;
        Animator = _animator;
        Player = _player;
        EnemyDetection = _enemyDetectionScript;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        Vector3 Destination = Player.position + (Random.insideUnitSphere * EnemyDetection.BattleSphereRadius);
        BattleStateMachine.TargetPosition = Destination;


        //Set Destination within Battle Range, but not behind Player
        NavMeshAgent.SetDestination(Destination);
        NavMeshAgent.isStopped = false;
        Animator.SetBool("isWalking", true);



    }

    public override void StateUpdate()
    {
        //Move to Destination
        //play Move Animaiton
    }

    public override void StateExit()
    {
        //Cancel Walk Animation and Movement
        NavMeshAgent.isStopped = true;
        Animator.SetBool("isWalking", false);
    }

}
