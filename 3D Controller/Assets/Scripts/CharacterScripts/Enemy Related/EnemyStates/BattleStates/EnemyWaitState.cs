using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaitState : EnemyBaseState
{
    public EnemyWaitState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMesh, Animator _animator, EnemyScript _enemyScript) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript)
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
