using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachineBase _enemyStateMachine, Animator _animator, NavMeshAgent _navMesh, EnemyScript _enemyScript) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript)
    {

    }


    public override void StateEnter()
    {
        StateMachine.transform.LookAt(StateMachine.PlayerPosition);
        StateMachine.IsAttacking = true;
        Attack();
    }

    public override void StateUpdate()
    {
    }

    public override void StateExit()
    {
    }


    public void Attack()
    {
        if (StateMachine.EnemyDetection.CheckRange(StateMachine.EnemyDetection.AttackSphereRadius) == true)
        {
            int randomAttackIndex = Random.Range(0, 2);

            if (randomAttackIndex == 0)
            {
                animator.SetTrigger("Attack Trigger");
            }
            else if (randomAttackIndex == 1)
            {
                animator.SetTrigger("Heavy Attack");
            }

        }

        else
        {
            RushAttack();
        }
    }


    private void RushAttack()
    {

        Debug.Log("Enemy has to Rush");
        //Set Destination To Player

        //When Attack Range is Reached: Attack

        //Set Trigger to Exit State
        StateMachine.IsAttacking = false;
    }
}
