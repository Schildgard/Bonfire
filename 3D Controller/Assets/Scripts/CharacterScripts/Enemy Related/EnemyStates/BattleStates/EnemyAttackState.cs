using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyBattleStateMachine _enemyStateMachine, Animator _animator, NavMeshAgent _navMesh) : base(_enemyStateMachine, _animator, _navMesh)
    {

    }


    public override void StateEnter()
    {

        base.StateEnter();
        Attack();
    }

    public override void StateUpdate()
    {


    }

    public override void StateExit()
    {
        //Debug.Log("Attack has been performed");
    }


    public void Attack()
    {

        int randomAttackIndex = Random.Range(0, 2);

        if (randomAttackIndex == 0)
        {
            animator.SetTrigger("Attack Trigger");
            //Debug.Log("Enemy performed normal Attack");
        }
        else if (randomAttackIndex == 1)
        {
            animator.SetTrigger("Heavy Attack");
           // Debug.Log("Enemy performed Heavy Attack");
        }
        navMesh.isStopped = true;

    }

}
