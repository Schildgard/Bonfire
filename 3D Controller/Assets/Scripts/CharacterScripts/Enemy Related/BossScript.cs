using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BossScript : EnemyScript
{
    [SerializeField]private  GameEvent bossDied;

    protected override void Start()
    {
        base.Start();
        Debug.Log("Health Script found!" + HealthScript.isAlive);
    }
    public override void Die()
    {
        base.Die();
        bossDied.Raise();
    }


}
