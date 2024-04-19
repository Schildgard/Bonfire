using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBattleState : EnemyBaseState, IAttackAction
{
    private NavMeshAgent NavMeshAgent;
    private Transform PlayerPosition;

    public EnemyBattleState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMeshAgent, Transform _playerPosition) : base(_enemyStateMachine)
    {
        NavMeshAgent = _navMeshAgent;
        PlayerPosition = _playerPosition;
    }


    public override void StateEnter()
    {
        Debug.Log("OnBattleEnter");
    }

    public override void StateUpdate()
    {
        Attack();
    }

    public override void StateExit()
    {
        Debug.Log("OnBattleExit");
    }

    protected void Attack() 
    {
        Debug.Log("Enemy Attack");
    }
}
