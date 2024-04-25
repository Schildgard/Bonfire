using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : EnemyStateMachineBase
{
    private EnemyBattleStateMachine EnemyBattleStateMachine;

    private Vector3 StartPosition;



    protected override void Awake()
    {
        base.Awake();
        StartPosition = transform.position;
        EnemyBattleStateMachine = GetComponent<EnemyBattleStateMachine>();
    }



    public override void InitializeStateMachine()
    {
        EnemyIdleState EnemyIdleState = new EnemyIdleState(this);
        EnemyChaseState EnemyChaseState = new EnemyChaseState(this, NavMeshAgent, PlayerPosition, Animator, this.transform);
        EnemyReturnState EnemyReturnState = new EnemyReturnState(this,NavMeshAgent, StartPosition, Animator);
        EnemyBattleState EnemyBattleState = new EnemyBattleState(this, EnemyBattleStateMachine);


        CurrentState = EnemyIdleState;
        CurrentState.StateEnter();
        // First Dictionary contains State and another Dictionary.   Second Dictionary contains Condition and TargetState
        EnemyStateDictionary = new Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>>
        {

             {
                  EnemyIdleState, new Dictionary<StateMachineDelegate,EnemyBaseState>
                  {
                    {   ()=>EnemyDetection.CheckRange(EnemyDetection.BattleSphereRadius),EnemyBattleState},
                    {   ()=>EnemyDetection.CheckRange(EnemyDetection.ChaseSphereRadius),EnemyChaseState}
                  }

             },


            {
                EnemyChaseState, new Dictionary<StateMachineDelegate,EnemyBaseState>
                {
                    {   ()=>EnemyDetection.CheckRange(EnemyDetection.BattleSphereRadius), EnemyBattleState},
                    {   ()=>!EnemyDetection.CheckRange(EnemyDetection.ChaseSphereRadius), EnemyReturnState}
                }
            },

            {
                EnemyBattleState, new Dictionary<StateMachineDelegate,EnemyBaseState>
                {
                    {   ()=>!EnemyDetection.CheckRange(EnemyDetection.BattleSphereRadius), EnemyChaseState},
                    {   ()=>!EnemyDetection.CheckRange(EnemyDetection.ChaseSphereRadius), EnemyReturnState}
                }
            },

            {
                EnemyReturnState, new Dictionary<StateMachineDelegate,EnemyBaseState>
                {
                   {   ()=>CompareDistance(StartPosition, transform.position) <= 1f, EnemyIdleState},

                }
            },

        };
    }




}
