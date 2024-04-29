using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    public float MaxHealth { get { return maxHealth; } set { MaxHealth = value; } }
    private bool isAlive;



    private StatScript Stats;
    private Animator Animator;
    private Collider characterCollider;
    [SerializeField] private EnemyStateMachineBase[] EnemyStateMachines;







    private void Awake()
    {
        Stats = GetComponent<StatScript>();
        Animator = GetComponent<Animator>();
        characterCollider = GetComponent<Collider>();
        if (gameObject.layer == 8)
        { EnemyStateMachines = GetComponents<EnemyStateMachineBase>(); }


    }

    private void Start()
    {
        UpdateMaxHealth();
        ResetHealth();
        isAlive = true;
    }




    public void GetDamage(float _damage)
    {
        float defMultiplier = (_damage / 100) * (Stats.Defense * 5f);
        if (!isAlive)
        { return; }
        currentHealth -= (_damage - defMultiplier);
        Debug.Log($"{gameObject.name} got {_damage - defMultiplier} Damage");
        if (currentHealth <= 0)
        { Die(); }
        Animator.SetTrigger("Get Damage");
    }




    public virtual void Die()
    {
        if (!isAlive)
        { return; }
        isAlive = false;

        Animator.SetTrigger("Died");
        Debug.Log(gameObject.name + "died");

        if (gameObject.layer == 7) // If Player dies
        {
            //set currentSouls to Zero
            //spawn soulsPool
            //spawn Player at last Bonfire
            return;
        }

        SoulsSystem.instance.GainSouls(Stats.SoulsValue);
        characterCollider.enabled = false;
        //drop Item
        //deactivate Behaviour
        foreach (var enemyStateMachine in EnemyStateMachines)
        {
            enemyStateMachine.enabled = false;
        }
    }





    public void UpdateMaxHealth()
    {
        maxHealth = Stats.Vitality * 10;
    }

    public void UpdateMaxHealth(float _value)
    {

        maxHealth += _value;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
