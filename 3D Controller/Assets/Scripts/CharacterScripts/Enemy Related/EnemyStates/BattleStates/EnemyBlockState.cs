using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBlockState : EnemyBaseState
{

    public EnemyBlockState(EnemyStateMachine _enemyStateMachine, Animator _animator, NavMeshAgent _navMesh, EnemyScript _enemyScript) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript)
    {
    }

    public override void StateEnter()
    {
        base.StateEnter();
        //Start Block Action and Animation

    }

    public override void StateUpdate()
    {
        //perform Block Action and Animation
    }

    public override void StateExit()
    {
        //End Block Action and Animation
    }
}
