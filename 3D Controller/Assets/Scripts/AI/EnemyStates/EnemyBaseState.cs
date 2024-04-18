using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    protected EnemyStateMachine StateMachine;

    public EnemyBaseState(EnemyStateMachine _enemyStateMachine) 
    {
        StateMachine = _enemyStateMachine;
    }

    public virtual void StateEnter() 
    { 

    }

    public virtual void StateUpdate()
    { 

    }

    public virtual void StateExit() 
    {

    }
}
