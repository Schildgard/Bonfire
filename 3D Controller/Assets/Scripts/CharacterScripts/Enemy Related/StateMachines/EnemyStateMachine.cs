using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : EnemyStateMachineBase
{
    private EnemyBattleStateMachine EnemyBattleStateMachine;
    private Vector3 StartPosition;

    #region MultiThreading
    private bool taskBool;
    public bool TaskBool { get => taskBool; set => taskBool = value; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        StartPosition = transform.position;
        EnemyBattleStateMachine = GetComponent<EnemyBattleStateMachine>();
    }



    public override void InitializeStateMachine()
    {
        EnemyIdleState EnemyIdleState = new EnemyIdleState(this, Animator, NavMeshAgent);
        EnemyChaseState EnemyChaseState = new EnemyChaseState(this, NavMeshAgent, PlayerPosition, Animator, this.transform);
        EnemyReturnState EnemyReturnState = new EnemyReturnState(this, NavMeshAgent, StartPosition, Animator);

        EnemyBattleState EnemyBattleState = new EnemyBattleState(this, EnemyBattleStateMachine, Animator, NavMeshAgent);

        EnemyAttackState EnemyAttackState = new EnemyAttackState(this, Animator, NavMeshAgent);  //Remove Battle StateMachine Later
        EnemyStrafingState EnemyStrafingState = new EnemyStrafingState(this, NavMeshAgent, Animator); //Check Paramters for correctness


        CurrentState = EnemyIdleState;
        CurrentState.StateEnter();


        // First Dictionary contains State and another Dictionary.   Second Dictionary contains Condition and TargetState
        EnemyStateDictionary = new Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>>
        {

             {
                  EnemyIdleState, new Dictionary<StateMachineDelegate,EnemyBaseState>
                  {
                    { ()=> GetRadius(transform, PlayerPosition) >= 0.7f,EnemyChaseState },
                    { () => EnemyDetection.CheckRange(EnemyDetection.AttackSphereRadius), EnemyAttackState }

                 // {
                 //   //MultiThreading
                 //   () => TaskBool, EnemyChaseState
                 // }
                  }

             },


            {
                EnemyChaseState, new Dictionary<StateMachineDelegate,EnemyBaseState>
                {
                    {   ()=>EnemyDetection.CheckRange(EnemyDetection.AttackSphereRadius), EnemyAttackState},
                    {   ()=>!EnemyDetection.CheckRange(EnemyDetection.ChaseSphereRadius), EnemyReturnState}
                }
            },

            {
                EnemyAttackState, new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    { ()=> EnemyDetection.CheckRange(EnemyDetection.AttackSphereRadius) && !isAttacking, EnemyAttackState},
                    { ()=> !EnemyDetection.CheckRange(EnemyDetection.AttackSphereRadius), EnemyChaseState},

                }

            },


            {
                EnemyStrafingState, new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    {   () => stateTimer <= 0f, EnemyAttackState  },
                    {   ()=>!EnemyDetection.CheckRange(EnemyDetection.BattleSphereRadius), EnemyChaseState},
                    {   ()=>!EnemyDetection.CheckRange(EnemyDetection.ChaseSphereRadius), EnemyReturnState}


                }

            },

            {
                EnemyBattleState, new Dictionary<StateMachineDelegate,EnemyBaseState>
                {
                    {   ()=>!EnemyDetection.CheckRange(EnemyDetection.BattleSphereRadius) && !isAttacking, EnemyChaseState},
                    {   ()=>!EnemyDetection.CheckRange(EnemyDetection.ChaseSphereRadius) && !isAttacking, EnemyReturnState}
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
