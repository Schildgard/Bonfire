using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgonizedEnemy_StateMachine : EnemyStateMachineBase
{
    private Vector3 StartPosition;
    private bool CallAnimationIsPlaying;

    private bool CallAnimationhasEnded;

    #region MultiThreading
    private bool taskBool;
    public bool TaskBool { get => taskBool; set => taskBool = value; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        StartPosition = transform.position;
    }



    public override void InitializeStateMachine()
    {
        AgonizedEnemy_IdleState EnemyIdleState = new AgonizedEnemy_IdleState(this, Animator, NavMeshAgent, enemyScript);
        AgonizedEnemy_CallForHelpState CallforHelpState = new AgonizedEnemy_CallForHelpState(this, Animator, NavMeshAgent, enemyScript);



        EnemyChaseState EnemyChaseState = new EnemyChaseState(this, NavMeshAgent, PlayerPosition, Animator, this.transform, enemyScript, EnemyDetection);
        EnemyReturnState EnemyReturnState = new EnemyReturnState(this, NavMeshAgent, StartPosition, Animator, enemyScript, EnemyDetection);

        EnemyAttackState EnemyAttackState = new EnemyAttackState(this, Animator, NavMeshAgent, enemyScript);
        EnemyStrafingState EnemyStrafingState = new EnemyStrafingState(this, NavMeshAgent, Animator, enemyScript);


        CurrentState = EnemyIdleState;
        CurrentState.StateEnter();


        // First Dictionary contains State and another Dictionary.   Second Dictionary contains Condition and TargetState
        EnemyStateDictionary = new Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>>
        {

             {
                  EnemyIdleState, new Dictionary<StateMachineDelegate,EnemyBaseState>
                  {
                      {()=> EnemyDetection.Detected, CallforHelpState }
                  }

             },


             {
                  CallforHelpState, new Dictionary<StateMachineDelegate,EnemyBaseState>
                  {
                      {()=> CallAnimationhasEnded, EnemyChaseState }
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
                    { ()=> !EnemyDetection.CheckRange(EnemyDetection.AttackSphereRadius) && !isAttacking, EnemyChaseState},

                }

            },


            {
                EnemyStrafingState, new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    {   () => stateTimer <= 0f, EnemyAttackState  },
                    {   ()=>!EnemyDetection.CheckRange(EnemyDetection.StrafingSphereRadius), EnemyChaseState},
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

    public void SetCallAnimationStartedBool()
    {
        CallAnimationIsPlaying = true;
    }
    public void SetCallAnimationBoolEnded()
    {
        CallAnimationhasEnded = true;
        Animator.SetTrigger("Chase");
    }



    public override void CheckAggressiveBehaviour()
    {
        if (EnemyDetection.Detected == false)
        {
            Collider[] col;
            col = Physics.OverlapSphere(transform.position, EnemyDetection.AlarmRadius, layerMask: EnemyDetection.EnemyLayer);

            foreach (var enemy in col)
            {
                if (enemy.gameObject.GetComponent<EnemyDetectionScript>().Detected)
                {
                    StartCoroutine(TriggerEnemy());
                    return;
                }
            }
        }
    }

    IEnumerator TriggerEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        EnemyDetection.Detected = true;
    }
}
