using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth;
    public float MaxHealth { get { return maxHealth; } set { MaxHealth = value; } }
    public bool isAlive;
    private StatScript Stats;





    private void Awake()
    {
        Stats = GetComponent<StatScript>();


    }

    private void Start()
    {
        UpdateMaxHealth();
        ResetHealth();
        isAlive = true;
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
