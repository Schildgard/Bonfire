using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : EnemyStateMachineBase
{
    //Start Position Variable is given to remember the position the enemy has to return to when entering return state.
    private Vector3 startPosition;


    protected override void Awake()
    {
        base.Awake();
        startPosition = transform.position;
    }



    public override void InitializeStateMachine()
    {
        EnemyIdleState EnemyIdleState = new EnemyIdleState(this, Animator, NavMeshAgent, enemyScript);
        EnemyChaseState EnemyChaseState = new EnemyChaseState(this, NavMeshAgent, PlayerPosition, Animator, this.transform, enemyScript, EnemyDetection);
        EnemyReturnState EnemyReturnState = new EnemyReturnState(this, NavMeshAgent, startPosition, Animator, enemyScript, EnemyDetection);

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
                    { ()=> GetRadius(transform, PlayerPosition) >= 0.7f,EnemyChaseState },
                    { () => EnemyDetection.CheckRange(EnemyDetection.AttackSphereRadius), EnemyChaseState },
                    { ()=> EnemyDetection.Detected,EnemyChaseState }
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
                   {   ()=>CompareDistance(startPosition, transform.position) <= 1f, EnemyIdleState},
                }
            },
        };
    }

    //This method is called by EnemySpotting Manager.
    public override void CheckAggressiveBehaviour()
    {
        if (EnemyDetection.Detected)
        {
            Collider[] col;
            col = Physics.OverlapSphere(transform.position, EnemyDetection.AlarmRadius, layerMask: EnemyDetection.EnemyLayer);

            if (col.Length > 0)
            {
                foreach (var enemy in col)
                {
                    enemy.gameObject.GetComponent<EnemyDetectionScript>().Detected = true;
                }
            }
        }
    }


}
