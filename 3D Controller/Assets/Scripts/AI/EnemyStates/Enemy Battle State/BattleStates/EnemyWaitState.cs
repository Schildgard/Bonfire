using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaitState : EnemyBaseState
{
    private NavMeshAgent NavMeshAgent;
    public EnemyWaitState(EnemyBattleStateMachine _enemyStateMachine, NavMeshAgent navMeshAgent) : base(_enemyStateMachine)
    {
        NavMeshAgent = navMeshAgent;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        NavMeshAgent.isStopped = true;
        Debug.Log("Enter Wait State");
    }

    public override void StateUpdate()
    {
        Debug.Log("Enemy Waits");
        //do nothing
    }

    public override void StateExit()
    {
        Debug.Log("Exit Wait State");
    }

}
