using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState
{
    protected EnemyStateMachine StateMachine;
    protected EnemyBattleStateMachine BattleStateMachine;

    protected Animator animator;
    protected NavMeshAgent navMesh;
    #region BlendTreeRelated
    protected float velocityX;
    protected float velocityZ;

    protected int velocityHashX;
    protected int velocityHashZ;

    protected float acceleration = 5f;
    protected float decceleration = 7f;

    protected float maxVelocity = 0.5f;
    protected float distanceTolerance = 0.05f;
    #endregion

    public EnemyBaseState(EnemyStateMachine _enemyStateMachine, Animator _animator, NavMeshAgent _navMesh)
    {
        StateMachine = _enemyStateMachine;
        animator = _animator;
        navMesh = _navMesh;
    }

    public EnemyBaseState(EnemyBattleStateMachine _enemyBattleStateMachine, Animator _animator, NavMeshAgent _navMesh)
    {
        BattleStateMachine = _enemyBattleStateMachine;
        animator = _animator;
        navMesh = _navMesh;

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
