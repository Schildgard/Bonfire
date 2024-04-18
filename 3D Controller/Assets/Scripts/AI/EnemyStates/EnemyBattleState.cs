using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleState : EnemyBaseState
{
    public EnemyBattleState(EnemyStateMachine _enemyStateMachine) : base(_enemyStateMachine)
    {

    }


    public override void StateEnter()
    {
        Debug.Log("OnBattleEnter");
    }

    public override void StateUpdate()
    {
        Debug.Log("OnBattleUpdate");
    }

    public override void StateExit()
    {
        Debug.Log("OnBattleExit");
    }
}
