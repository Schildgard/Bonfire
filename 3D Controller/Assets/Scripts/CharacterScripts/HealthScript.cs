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
        maxHealth = 950 + Stats.Vitality *50;
        UpdateHealthBar();
    }

    public void IncreaseMaxHealth(float _value)
    {

        maxHealth += _value;
        UpdateHealthBar();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }


    public void UpdateHealthBar()
    {
        Debug.Log(gameObject.name + "Healthbar updated");
        Healthbar.fillAmount = (1 / maxHealth) * currentHealth;
    }
}
