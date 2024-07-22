using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaitState : EnemyBaseState
{
    public EnemyWaitState(EnemyBattleStateMachine _enemyStateMachine, NavMeshAgent _navMesh, Animator _animator) : base(_enemyStateMachine, _animator, _navMesh)
    {
    }

    public override void StateEnter()
    {
        base.StateEnter();
        navMesh.isStopped = true;
        //Debug.Log("Enter Wait State");
    }

    public override void StateUpdate()
    {
        //Debug.Log("Enemy Waits");
        //do nothing
    }

    public override void StateExit()
    {
        //Debug.Log("Exit Wait State");
    }

}
