using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterScript : MonoBehaviour, IDamageable
{

    protected HealthScript HealthScript;
    protected StatScript Stats;
    protected Animator Animator;
    protected Collider Collider;

    [SerializeField] private int getDamageSoundIndex;
    [SerializeField] private int deathSoundIndex;

    protected virtual void Start()
    {
        HealthScript = GetComponent<HealthScript>();
        Stats = GetComponent<StatScript>();
        Animator = GetComponent<Animator>();
        Collider = GetComponent<Collider>();
    }


    public virtual void GetDamage(float _damage)
    {
        if (!HealthScript.isAlive)
        { return; }


        float defMultiplier = (_damage / 100) * (Stats.Defense * 3f);
        HealthScript.currentHealth -= (_damage - defMultiplier);
        HealthScript.UpdateHealthBar();

        Debug.Log($"{gameObject.name} got {_damage - defMultiplier} Damage");

        Animator.SetTrigger("Get Damage");
        if (HealthScript.currentHealth <= 0)
        { Die(); }
        else 
        {
            AudioManager.instance.SFX[getDamageSoundIndex].source.Play();
        }
    }

    public virtual void Respawn() { }

    public virtual void Die() 
    {
        if (!HealthScript.isAlive)
        { return; }
        HealthScript.isAlive = false;

        Animator.SetTrigger("Died");
        AudioManager.instance.SFX[deathSoundIndex].source.Play();
        Debug.Log(gameObject.name + "died");
    }

}
