using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBattleStateMachine : EnemyStateMachineBase
{
    private EnemyDetectionScript EnemyDetection;

    private NavMeshAgent NavMeshAgent;
    private Animator Animator;

    private float stateTimer;
    public float StateTimer 
    {
        get {return stateTimer;}
        set { stateTimer = value;}
    }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        EnemyDetection = GetComponent<EnemyDetectionScript>();

    }

    public override void InitializeStateMachine()
    {

        EnemyWaitState EnemyWaitState = new EnemyWaitState(this,NavMeshAgent);
        EnemyGoAroundState EnemyGoAroundState = new EnemyGoAroundState(this, NavMeshAgent, Animator, EnemyDetection);
        EnemyAttackState EnemyAttackState = new EnemyAttackState(this,Animator);
        EnemyBlockState EnemyBlockState = new EnemyBlockState(this);

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
                    { () => stateTimer <=0, EnemyWaitState},

                }
            },

            {
                EnemyAttackState,
                new Dictionary<StateMachineDelegate, EnemyBaseState>
                {
                    //{ () => !Animator.GetCurrentAnimatorStateInfo(0).IsName("ataka2"), EnemyGoAroundState},
                    //{() => Animator.GetAnimatorTransitionInfo(0).IsName("ExitAttack"), EnemyGoAroundState}
                    //{() => Animator.GetCurrentAnimatorClipInfo(0). }

                }
            },
       };

    }

    public override void UpdateStateMachine() 
    {
        stateTimer -= Time.deltaTime;
        base.UpdateStateMachine();

    }
}
