using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    private StatScript Stats;
    public Image StaminaBar;
    [SerializeField] private float maxStamina;
    [SerializeField]private float regMultiplier;


    [SerializeField]private float currentStamina;
    public float CurrentStamina { get { return currentStamina; } set { currentStamina = value; } }

    private void Awake()
    {
        Stats = GetComponent<StatScript>();
    }

    private void Update()
    {
        if (currentStamina < maxStamina && CurrentStamina >=1f)
        {
            RegenerateStamina();
        }
        else if (CurrentStamina < 1f)
        { 
            StartCoroutine(ExhaustionRegeneration());
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

    private IEnumerator ExhaustionRegeneration()
    {
        yield return new WaitForSeconds(1);

        CurrentStamina = 1;
        UpdateStaminaBar();
    }
}
