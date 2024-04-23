using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBattleState : EnemyBaseState, IAttackAction, IBlockAction
{
    private NavMeshAgent NavMeshAgent;
    private Transform PlayerPosition;
    private Animator Animator;
    enum ECurrentBattleState
    {
        lureAround,
        Attack,
        Block
    }
    private ECurrentBattleState CurrentBattleState;
    private float actionTimer = 0;

    public EnemyBattleState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Transform _playerPosition, Animator animator) : base(_enemyStateMachine)
    {
        NavMeshAgent = _navMeshAgent;
        PlayerPosition = _playerPosition;
        Animator = animator;
    }


    public override void StateEnter()
    {
        CurrentBattleState = ECurrentBattleState.lureAround;
        NavMeshAgent.isStopped = true;
        //  Debug.Log("OnBattleEnter");
    }

    public override void StateUpdate()
    {

        LureAround();
        actionTimer -= Time.deltaTime;
        if (actionTimer > 0) return;





        switch (CurrentBattleState)
        {
            case ECurrentBattleState.lureAround:
                Attack();
                break;
            default:
                break;
        }



    }

    public override void StateExit()
    {
        // Debug.Log("OnBattleExit");
    }


    public void Attack()
    {
        Debug.Log("Enemy Attacks");
        Animator.SetTrigger("Attack Trigger");
        ResetActionTimer();

    }

    public void Block()
    {
        Debug.Log("Enemy Blocks");
    }

    public void LureAround()
    {
        Debug.Log("Gegner lungert rum");
    }

    private void SetNewBattleState(ECurrentBattleState _targetBattleState)
    {
        CurrentBattleState = _targetBattleState;
        ResetActionTimer();
    }

    private void ResetActionTimer() 
    {
        actionTimer = Random.Range(2, 7);
        Debug.Log("New Action Timer is: " + actionTimer);
    }

}
