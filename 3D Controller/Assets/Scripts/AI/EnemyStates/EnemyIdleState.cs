using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    public EnemyIdleState(EnemyStateMachine _enemyStateMachine) : base(_enemyStateMachine) 
    {

    }


    public override void StateEnter()
    {
        Debug.Log("OnIdleEnter");
    }

    public override void StateUpdate()
    {
        Debug.Log("OnIdleUpdate");
    }

    public override void StateExit()
    {
        Debug.Log("OnIdleExit");
    }
}
