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
        StateMachine.StateTimer = Random.Range(5f, 16f);
    }

    public override void StateUpdate()
    {
        //if timer runs down > Set Animation Trigger Agonized
        StateMachine.StateTimer -= Time.deltaTime;
        if (StateMachine.StateTimer <= 0)
        {
            animator.SetTrigger("Agony");
            StateMachine.StateTimer = Random.Range(5f, 16f);
        }    

        
    }

    public override void StateExit()
    {
        //Call For Help
        
    }
}
