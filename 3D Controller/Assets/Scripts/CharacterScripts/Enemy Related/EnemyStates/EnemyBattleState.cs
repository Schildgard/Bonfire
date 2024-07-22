using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBattleState : EnemyBaseState
{



    private EnemyBattleStateMachine EnemyBattleStateMachine;

    public EnemyBattleState(EnemyStateMachine _enemyStateMachine, EnemyBattleStateMachine _enemyBattleStateMachine, Animator _animator, NavMeshAgent _navMesh) : base(_enemyStateMachine, _animator, _navMesh   )
    {
        EnemyBattleStateMachine = _enemyBattleStateMachine;
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
