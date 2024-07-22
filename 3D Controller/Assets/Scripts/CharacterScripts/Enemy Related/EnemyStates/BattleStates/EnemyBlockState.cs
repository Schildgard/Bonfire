using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBlockState : EnemyBaseState
{

    public EnemyBlockState(EnemyBattleStateMachine _enemyStateMachine, Animator _animator, NavMeshAgent _navMesh) : base(_enemyStateMachine, _animator, _navMesh    )
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
