using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    [SerializeField] private EnemyStateMachineBase[] EnemyStateMachines;

    protected override void Start()
    {
        base.Start();
        EnemyStateMachines = GetComponents<EnemyStateMachineBase>();
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
