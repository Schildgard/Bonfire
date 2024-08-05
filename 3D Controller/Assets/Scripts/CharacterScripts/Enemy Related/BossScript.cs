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
    }

    public override void GetDamage(float _damage)
    {
        if (!HealthScript.isAlive)
        { return; }

        float defMultiplier = (_damage / 100) * (Stats.Defense * 3f);
        HealthScript.currentHealth -= (_damage - defMultiplier);
        HealthScript.UpdateHealthBar();

        Debug.Log($"{gameObject.name} got {_damage - defMultiplier} Damage ({_damage} - {defMultiplier})");

        Animator.SetTrigger("Stagger");
        if (HealthScript.currentHealth <= 0)
        { Die(); }

        PlaySFXSound("Get Hit");
    }

    public override void Die()
    {
        base.Die();
        bossDied.Raise();
    }


}
