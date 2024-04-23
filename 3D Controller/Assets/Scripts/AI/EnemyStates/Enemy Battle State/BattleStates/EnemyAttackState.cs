using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState, IAttackAction
{
    private Animator Animator;
    public EnemyAttackState(EnemyBattleStateMachine _enemyStateMachine, Animator animator) : base(_enemyStateMachine)
    {
        Animator = animator;
    }


    public override void StateEnter()
    {
        
        //Choose Attack
    }

    public override void StateUpdate()
    {
        Attack();
        //perform Attack and start Animation
    }

    public override void StateExit()
    {
        Debug.Log("Attack has been performed");
    }

    public void Attack()
    {
        Debug.Log("Enemy Attacks");
        Animator.SetTrigger("Attack Trigger");

    }

}
