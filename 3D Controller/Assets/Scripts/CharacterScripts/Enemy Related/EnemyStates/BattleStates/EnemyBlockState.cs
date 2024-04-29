using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlockState : EnemyBaseState
{

    public EnemyBlockState(EnemyBattleStateMachine _enemyStateMachine) : base(_enemyStateMachine)
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
