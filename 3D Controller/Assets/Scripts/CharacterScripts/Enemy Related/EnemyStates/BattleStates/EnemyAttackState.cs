using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private Animator Animator;
    public EnemyAttackState(EnemyBattleStateMachine _enemyStateMachine, Animator animator) : base(_enemyStateMachine)
    {
        Animator = animator;
    }


    public override void StateEnter()
    {

        base.StateEnter();
        //Choose Attack
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
            Animator.SetTrigger("Attack Trigger");
            //Debug.Log("Enemy performed normal Attack");
        }
        else if (randomAttackIndex == 1)
        {
            Animator.SetTrigger("Heavy Attack");
           // Debug.Log("Enemy performed Heavy Attack");
        }

    }

}
