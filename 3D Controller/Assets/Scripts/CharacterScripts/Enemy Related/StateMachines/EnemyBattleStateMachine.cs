using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBattleStateMachine : EnemyStateMachineBase
{

    private Vector3 targetPosition;
    public Vector3 TargetPosition
    {
        get { return targetPosition; }
        set { targetPosition = value; }
    }

    private float stateTimer;
    public float StateTimer 
    {
        get {return stateTimer;}
        set { stateTimer = value;}
    }



    public override void InitializeStateMachine()
    {

        EnemyWaitState EnemyWaitState = new EnemyWaitState(this,NavMeshAgent, Animator);
        EnemyGoAroundState EnemyGoAroundState = new EnemyGoAroundState(this, NavMeshAgent, Animator, EnemyDetection, PlayerPosition);
        EnemyAttackState EnemyAttackState = new EnemyAttackState(this,Animator, NavMeshAgent);
        EnemyBlockState EnemyBlockState = new EnemyBlockState(this, Animator, NavMeshAgent);

        CurrentState = EnemyWaitState;
        CurrentState.StateEnter();

        EnemyStateDictionary = new Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>>()
        {
            {
                EnemyWaitState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    { () => stateTimer <=0, EnemyAttackState},

                }
            },

            {
                EnemyGoAroundState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    {() => CompareDistance(transform.position, targetPosition) <= 0.5f, EnemyWaitState}

                }
            },

            {
                EnemyAttackState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    {() => stateTimer <= 0, EnemyGoAroundState}
                }
            },
       };

    }

    public override void UpdateStateMachine() 
    {
        stateTimer -= Time.deltaTime;
        transform.LookAt(PlayerPosition);
        base.UpdateStateMachine();



    }


    public void AttackAnimationEnd() 
    {
        stateTimer = 0;
        

    }

    void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;
       // Gizmos.DrawRay(TargetPosition, Vector3.up *5f);

    }
}
