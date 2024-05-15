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
    private Image Healthbar;





    private void Awake()
    {
        Stats = GetComponent<StatScript>();
        Healthbar = GetComponentInChildren<Image>();


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
        Healthbar.fillAmount = (MaxHealth / 100) * currentHealth;
    }

    public void UpdateMaxHealth(float _value)
    {

        maxHealth += _value;
        Healthbar.fillAmount = (MaxHealth / 100) * currentHealth;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        Healthbar.fillAmount = (MaxHealth / 100) * currentHealth;
    }
}
