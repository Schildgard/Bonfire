using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour, IDamageable
{
    protected HealthScript HealthScript;
    protected StatScript Stats;
    protected Animator Animator;
    protected Collider Collider;
    // Start is called before the first frame update

    protected virtual void Start()
    {
        HealthScript = GetComponent<HealthScript>();
        Stats = GetComponent<StatScript>();
        Animator = GetComponent<Animator>();
        Collider = GetComponent<Collider>();
    }


    public void GetDamage(float _damage)
    {
        if (!HealthScript.isAlive)
        { return; }


        float defMultiplier = (_damage / 100) * (Stats.Defense * 5f);
        HealthScript.currentHealth -= (_damage - defMultiplier);

        Debug.Log($"{gameObject.name} got {_damage - defMultiplier} Damage");

        if (HealthScript.currentHealth <= 0)
        { Die(); }
        Animator.SetTrigger("Get Damage");
    }

    public virtual void Die() 
    {
        if (!HealthScript.isAlive)
        { return; }
        HealthScript.isAlive = false;

        Animator.SetTrigger("Died");
        Debug.Log(gameObject.name + "died");
    }

}
