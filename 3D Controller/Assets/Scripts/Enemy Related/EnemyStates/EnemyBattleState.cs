using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBattleState : EnemyBaseState
{



    private EnemyBattleStateMachine EnemyBattleStateMachine;

    public EnemyBattleState(EnemyStateMachine _enemyStateMachine, EnemyBattleStateMachine _enemyBattleStateMachine) : base(_enemyStateMachine)
    {
        EnemyBattleStateMachine = _enemyBattleStateMachine;
    }


    public override void StateEnter()
    {

        EnemyBattleStateMachine.enabled = true;
        
    }

    public override void StateUpdate()
    {


    }

    public override void StateExit()
    {
        EnemyBattleStateMachine.enabled = false;

    }


}
