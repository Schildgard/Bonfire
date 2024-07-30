using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBattleState : EnemyBaseState
{



    private EnemyBattleStateMachine EnemyBattleStateMachine;

    public EnemyBattleState(EnemyStateMachine _enemyStateMachine, EnemyStateMachine _enemyBattleStateMachine, Animator _animator, NavMeshAgent _navMesh, EnemyScript _enemyScript) : base(_enemyStateMachine, _animator, _navMesh, _enemyScript)
    {
    }


    public override void StateEnter()
    {

        EnemyBattleStateMachine.enabled = true;
        
    }

    public override void StateUpdate()
    {


    }

    public override void StateExit()
    {
        EnemyBattleStateMachine.enabled = false;

    }


}
