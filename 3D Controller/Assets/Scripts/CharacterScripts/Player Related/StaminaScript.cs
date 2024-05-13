using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    private StatScript Stats;
    public Image StaminaBar;
    [SerializeField] private float maxStamina;


    [SerializeField]private float currentStamina;
    public float CurrentStamina { get { return currentStamina; } set { currentStamina = value; } }
    [SerializeField]private float regMultiplier;

    private void Awake()
    {
        Stats = GetComponent<StatScript>();
    }

    private void Update()
    {
        if (currentStamina < maxStamina)
        {
            RegenerateStamina();
        }
    }

    private void RegenerateStamina() 
    {
        currentStamina += Time.deltaTime * regMultiplier;
        UpdateStaminaBar();
    }

    public void UpdateStaminaBar()
    {
        StaminaBar.fillAmount = (maxStamina / 10000) * currentStamina;
    }

    
}
