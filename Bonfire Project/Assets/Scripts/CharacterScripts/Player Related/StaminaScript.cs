using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{

    private StatScript stats;
    public Image StaminaBar;
    [SerializeField] private float maxStamina;
    [SerializeField] private float regMultiplier;
    [SerializeField] private float currentStamina;


    private bool regenerationBlocked;
    public float CurrentStamina { get { return currentStamina; } set { currentStamina = value; } }


    private void Awake()
    {
        stats = GetComponent<StatScript>();
    }
    private void Start()
    {
        UpdateMaxStamina();
    }

    private void Update()
    {
        if (!regenerationBlocked)
        {

            if (currentStamina < maxStamina)
            {
                RegenerateStamina();
            }
        }
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            StartCoroutine(ExhaustionRegeneration());
        }
    }



    public void UpdateMaxStamina()
    {
        maxStamina = 90 + stats.Speed * 10;
        regMultiplier = 20 + stats.Speed * 5;
        UpdateStaminaBar();
    }
    private void RegenerateStamina()
    {
        currentStamina += Time.deltaTime * regMultiplier;
        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
        }
        UpdateStaminaBar();
    }

    public void DrainStamina(float _staminaCost)
    {
        currentStamina -= _staminaCost;
        UpdateStaminaBar();
    }

    public void BlockStaminaRegeneration()
    {
        if (!regenerationBlocked)
        {
            regenerationBlocked = true;
        }
    }
    public void ContinueStaminaRegeneration()
    {
        if (regenerationBlocked)
        {
            regenerationBlocked = false;
        }
    }

    public void UpdateStaminaBar()
    {
        //StaminaBar.fillAmount = (maxStamina / 10000) * currentStamina;
        StaminaBar.fillAmount = (1 / maxStamina) * currentStamina;
    }

    private IEnumerator ExhaustionRegeneration()
    {
        regenerationBlocked = true;
        yield return new WaitForSeconds(3);
        regenerationBlocked = false;
    }
}
