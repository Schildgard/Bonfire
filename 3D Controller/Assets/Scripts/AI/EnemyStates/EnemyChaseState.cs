using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine _enemyStateMachine) : base(_enemyStateMachine)
    {

    }


    public override void StateEnter()
    {
        Debug.Log("OnChaseEnter");
    }

    public override void StateUpdate()
    {
        Debug.Log("OnChaseUpdate");
    }

    public override void StateExit()
    {
        Debug.Log("OnChaseExit");
    }
}
