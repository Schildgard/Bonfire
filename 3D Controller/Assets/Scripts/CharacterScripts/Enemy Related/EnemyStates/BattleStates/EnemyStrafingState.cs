using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyStrafingState : EnemyBaseState
{
    private Vector3 Destination;


    public EnemyStrafingState(EnemyStateMachine _enemyStateMachine, NavMeshAgent _navMesh, Animator _animator) : base(_enemyStateMachine, _animator, _navMesh)
    {


    }

    public override void StateEnter()
    {

        Debug.Log("Enter Strafing State");
        //  Destination = BattleStateMachine.EnemyPosition.position + new Vector3(BattleStateMachine.EnemyPosition.right.x *Random.Range(-2, 3), 0, BattleStateMachine.EnemyPosition.forward.z * Random.Range(0, 2));
        //  BattleStateMachine.TargetPosition = Destination;


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
        //
        //  if (DistanceToDestination.x > 0f + distanceTolerance) // Comparison of float Vector3! Exchange with Distance formular!!
        //  {
        //      //Increase BlendTree Velocity X
        //      velocityX = Mathf.Clamp(velocityX + Time.deltaTime * acceleration, velocityX, maxVelocity);
        //  }
        //  else if (DistanceToDestination.x < 0f - distanceTolerance) 
        //  {
        //      //Decrease BlendTree Velocity X
        //      velocityX = Mathf.Clamp(velocityX - Time.deltaTime * acceleration, -maxVelocity, velocityX);
        //  }
        //  else
        //  {
        //      velocityX = velocityX = 0f;
        //  }
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
