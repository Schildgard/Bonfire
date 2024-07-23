using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyBaseState
{

    public EnemyIdleState(EnemyStateMachine _enemyStateMachine, Animator _animator, NavMeshAgent _navMesh) : base(_enemyStateMachine, _animator, _navMesh) 
    {

    }


    public override void StateEnter()
    {
        Debug.Log("OnIdleEnter");
    }

    public override void StateUpdate()
    {
       // Debug.Log("OnIdleUpdate");
    }

    public override void StateExit()
    {
       // Debug.Log("OnIdleExit");
    }
}
