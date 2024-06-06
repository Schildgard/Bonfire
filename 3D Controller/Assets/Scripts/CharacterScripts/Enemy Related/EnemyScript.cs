using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    [SerializeField] private EnemyStateMachineBase[] EnemyStateMachines;

    private Vector3 SpawnPosition;

    protected override void Start()
    {
        base.Start();
        EnemyStateMachines = GetComponents<EnemyStateMachineBase>();
        SpawnPosition = transform.position;
    }

    public override void Respawn()
    {
        transform.position = SpawnPosition;
        Collider.enabled = true;
       // foreach (var enemyStateMachine in EnemyStateMachines)
       // {
       //     enemyStateMachine.enabled = true;
       // }
        Animator.SetTrigger("Respawn");
        HealthScript.ResetHealth();
        HealthScript.isAlive = true;
    }

    public override void Die()
    {
        base.Die();
        SoulsSystem.instance.GainSouls(Stats.SoulsValue);
        Collider.enabled = false;
        //drop Item
        foreach (var enemyStateMachine in EnemyStateMachines)
        {
            enemyStateMachine.enabled = false;
        }
    }
}
