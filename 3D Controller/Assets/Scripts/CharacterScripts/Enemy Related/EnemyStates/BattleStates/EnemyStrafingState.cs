using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyStrafingState : EnemyBaseState
{
    private Vector3 Destination;


    public EnemyStrafingState(EnemyStateMachineBase _enemyStateMachine, NavMeshAgent _navMesh, Animator _animator, EnemyScript _enemyScript) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript)
    {


    }

    public override void StateEnter()
    {
        //Set Destination within Battle Range, but not behind Player
        StateMachine.StateTimer = Random.Range(1f, 8f);
        Destination = StateMachine.transform.localPosition - new Vector3(0, 0, Time.deltaTime * 5f);





        navMesh.SetDestination(Destination);
        navMesh.isStopped = false;

        velocityHashX = Animator.StringToHash("VelocityX");
        velocityHashZ = Animator.StringToHash("VelocityZ");

    }

    public override void StateUpdate()
    {
        StateMachine.transform.LookAt(StateMachine.PlayerPosition);
        StateMachine.transform.position = new Vector3(0, 0, Time.deltaTime * 2f);
        StateMachine.StateTimer -= Time.deltaTime;


        Debug.Log("Strafe Update");


        Vector3 DistanceToDestination = StateMachine.transform.InverseTransformPoint(Destination);

        if (DistanceToDestination.z > 0f + distanceTolerance)
        {
            //Increase BlendTree Velocity Y
            velocityZ = Mathf.Clamp(velocityZ + Time.deltaTime * acceleration, velocityZ, maxVelocity);
        }
        else if (DistanceToDestination.z < 0f - distanceTolerance)
        {
            //Decrease BlendTree Velocity Y
            velocityZ = Mathf.Clamp(velocityZ - Time.deltaTime * acceleration, -maxVelocity, velocityZ);
        }
          else
          {
              velocityZ = velocityZ = 0f;
          }
        
        animator.SetFloat(velocityHashX, velocityX);
        animator.SetFloat(velocityHashZ, velocityZ);


    }

    public override void StateExit()
    {
        navMesh.isStopped = true;
    }
}
