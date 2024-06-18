using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    protected EnemyStateMachine StateMachine;
    protected EnemyBattleStateMachine BattleStateMachine;

    public EnemyBaseState(EnemyStateMachine _enemyStateMachine)
    {
        StateMachine = _enemyStateMachine;
    }

    public EnemyBaseState(EnemyBattleStateMachine _enemyBattleStateMachine)
    {
        BattleStateMachine = _enemyBattleStateMachine;

    }

    public virtual void StateEnter()
    {
        if(BattleStateMachine != null)
        {
            BattleStateMachine.StateTimer = Random.Range(1, 4);
        }
    }

    public virtual void StateUpdate()
    {

    }

    public virtual void StateExit()
    {

    }
}
