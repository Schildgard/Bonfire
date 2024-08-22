using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float CurrentHealth;
    public float MaxHealth { get { return maxHealth; } set { MaxHealth = value; } }
    public bool isAlive;
    private StatScript stats;
    [SerializeField] private Image healthbar;



    private void Awake()
    {
        stats = GetComponent<StatScript>();
    }

    private void Start()
    {
        UpdateMaxHealth();
        ResetHealth();
        isAlive = true;
    }




    public void UpdateMaxHealth()
    {
        maxHealth = 950 + stats.Vitality *50;
        UpdateHealthBar();
    }

    public void IncreaseMaxHealth(float _value)
    {

        maxHealth += _value;
        UpdateHealthBar();
    }

    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        UpdateHealthBar();
    }


    public void UpdateHealthBar()
    {
        healthbar.fillAmount = (1 / maxHealth) * CurrentHealth;
    }
}
