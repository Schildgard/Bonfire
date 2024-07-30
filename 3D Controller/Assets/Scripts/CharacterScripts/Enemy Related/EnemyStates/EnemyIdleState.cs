using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachineBase _enemyStateMachine, Animator _animator, NavMeshAgent _navMesh, EnemyScript _enemyScript) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript) 
    {
        
    }


    public override void StateEnter()
    {
    }

    public override void StateUpdate()
    {
       // Debug.Log("OnIdleUpdate");
    }

    public override void StateExit()
    {
        enemyScript.PlaySoundSFX(0);
    }
}
