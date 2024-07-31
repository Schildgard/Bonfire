using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgonizedEnemy_IdleState : EnemyBaseState
{
    public AgonizedEnemy_IdleState(EnemyStateMachineBase _enemyStateMachine, Animator _animator, NavMeshAgent _navMesh, EnemyScript _enemyScript) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript)
    {

    }

    public override void StateEnter()
    {
    }

    public override void StateUpdate()
    {
        //Check Health of Nearby Enemies
        
    }

    public override void StateExit()
    {
        //Call For Help
        animator.SetTrigger("Attention");
    }
}
