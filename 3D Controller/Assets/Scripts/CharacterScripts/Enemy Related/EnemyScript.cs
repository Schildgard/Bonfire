using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    [SerializeField] private int enemyID;


    public int EnemyID => enemyID;


    public override void Die()
    {
        base.Die();
        SoulsSystem.instance.GainSouls(Stats.SoulsValue);
        Collider.enabled = false;

    }


 
}
