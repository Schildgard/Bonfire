using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] EnemyDetectionScript EnemyDetection;


    private NavMeshAgent NavMeshAgent;
    private EnemyBaseState currentState;
    private Vector3 StartPosition;

    public delegate bool StateMachineDelegate();
    private Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>> EnemyStateDictionary;


    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        StartPosition = transform.position;
    }
    void Start()
    {
        //First Initialization of State Machine (Assign States, set current State and enter to it
        InitializeStateMachine();
    }

    // Update is called once per frame
    void Update()
    {
        // Continous Update of State Machine. Exercise current State. Check if any Conditions for State Change are given and Change.
        UpdateStateMachine();
    }


    private void InitializeStateMachine()
    {
        EnemyIdleState EnemyIdleState = new EnemyIdleState(this);
        EnemyChaseState EnemyChaseState = new EnemyChaseState(this, NavMeshAgent, PlayerTransform);
        EnemyBattleState EnemyBattleState = new EnemyBattleState(this, NavMeshAgent, PlayerTransform);
        EnemyReturnState EnemyReturnState = new EnemyReturnState(this,NavMeshAgent, StartPosition);

        currentState = EnemyIdleState;
        currentState.StateEnter();
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


    private void UpdateStateMachine()
    {
        currentState.StateUpdate();
        //if condition for ANY StateChange from current is met: Transit!
        //look in current State for transition Conditions all the time (foreach)
        //Key() checks if the conditional Delegate is true

        foreach (var transition in EnemyStateDictionary[currentState])
        {
            if (transition.Key() == true)
            {
                currentState.StateExit();
                currentState = transition.Value;
                currentState.StateEnter();
            }
        }
    }

    private float CompareDistance(Vector3 _currentPosition, Vector3 _targetPosition) 
    {
        Vector3 DistanceVector = _targetPosition - _currentPosition;
        float distanceToTarget = Vector3.SqrMagnitude(DistanceVector);
        return distanceToTarget;
    }
}
