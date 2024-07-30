using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState
{
    protected EnemyStateMachine StateMachine;

    protected Animator animator;
    protected NavMeshAgent navMesh;
    protected EnemyScript enemyScript;
    #region BlendTreeRelated
    protected float velocityX;
    protected float velocityZ;

    protected int velocityHashX;
    protected int velocityHashZ;

    protected float acceleration = 1f;
    protected float decceleration = 1.5f;

    protected float maxVelocity = 0.5f;
    protected float distanceTolerance = 0.05f;
    #endregion

    public EnemyBaseState(EnemyStateMachine _enemyStateMachine, Animator _animator, NavMeshAgent _navMesh, EnemyScript _enemyScript)
    {
        StateMachine = _enemyStateMachine;
        animator = _animator;
        navMesh = _navMesh;
        enemyScript = _enemyScript;
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
