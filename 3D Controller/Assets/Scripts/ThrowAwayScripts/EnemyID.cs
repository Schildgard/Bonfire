using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyID : MonoBehaviour
{
    public int enemyID;

    public Vector3 SpawnPosition;

    public HealthScript HealthScript;

    public EnemyStateMachine StateMachine;

    public EnemyBattleStateMachine BattleStateMachine;

    private void Awake()
    {
        SpawnPosition = transform.position;
        HealthScript = GetComponent<HealthScript>();
        StateMachine = GetComponent<EnemyStateMachine>();
        BattleStateMachine = GetComponent<EnemyBattleStateMachine>();
    }


    public void ResetEnemy() 
    {
        transform.position = SpawnPosition;
        HealthScript.ResetHealth();
        StateMachine.enabled = true;
        
    }
}
