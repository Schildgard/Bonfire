using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyStateMachineBase : MonoBehaviour
{
    public delegate bool StateMachineDelegate();
    public Dictionary<EnemyBaseState, Dictionary<StateMachineDelegate, EnemyBaseState>> EnemyStateDictionary;
    public EnemyBaseState CurrentState;
    public Transform PlayerPosition;



    public  void Start()
    {
        //First Initialization of State Machine (Assign States, set current State and enter to it
        InitializeStateMachine();
    }

    // Update is called once per frame
    public void Update()
    {
        // Continous Update of State Machine. Exercise current State. Check if any Conditions for State Change are given and Change.
        UpdateStateMachine();
    }


    public virtual void InitializeStateMachine()
    {

    }


    public virtual void UpdateStateMachine()
    {
        CurrentState.StateUpdate();
        //if condition for ANY StateChange from current is met: Transit!
        //look in current State for transition Conditions all the time (foreach)
        //Key() checks if the conditional Delegate is true

        foreach (var transition in EnemyStateDictionary[CurrentState])
        {
            if (transition.Key() == true)
            {
                CurrentState.StateExit();
                CurrentState = transition.Value;
                CurrentState.StateEnter();
            }
        }
    }
}
