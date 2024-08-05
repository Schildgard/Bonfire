using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    [SerializeField] private int enemyID;
    [SerializeField]protected GameObject lifeCanvas;


    public int EnemyID => enemyID;

    protected override void Start()
    { 
        base.Start();
    }
    public override void Die()
    {
        base.Die();
        SoulsSystem.instance.GainSouls(Stats.SoulsValue);
        Collider.enabled = false;

    }

    public override void GetDamage(float _damage)
    {
        if (!HealthScript.isAlive)
        { return; }

        if (!lifeCanvas.activeSelf)
        { 
            lifeCanvas.SetActive(true);
        }


        float defMultiplier = (_damage / 100) * (Stats.Defense * 3f);
        HealthScript.currentHealth -= (_damage - defMultiplier);
        HealthScript.UpdateHealthBar();

        Debug.Log($"{gameObject.name} got {_damage - defMultiplier} Damage ({_damage} - {defMultiplier})");

        Animator.SetTrigger("Stagger");
        if (HealthScript.currentHealth <= 0)
        { Die(); }

        PlaySFXSound("Get Hit");
    }



}
