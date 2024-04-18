using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturnState : EnemyBaseState
{
    public EnemyReturnState(EnemyStateMachine _enemyStateMachine) : base(_enemyStateMachine) 
    {
    
    }


    public override void StateEnter()
    {
        Debug.Log("OnReturnEnter");
    }

    public override void StateUpdate()
    {
        Debug.Log("OnReturnUpdate");
    }

    public override void StateExit()
    {
        Debug.Log("OnReturnExit");
    }

}
