using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    //This script shows the Player Stats when leveling up.
    [SerializeField] private TMP_Text[] Attributes;
    [SerializeField] private StatScript PlayerStats;
    [SerializeField] private HealthScript PlayerHealthScript;
    [SerializeField] private StaminaScript PlayerStamina;
    [SerializeField] private GameObject StatCanvas;


    // These are copies of the Players original Stats. In the UI the Player only Changes the values on these copies, which are applied to the player after the Assign button is pressed.
    private float levelReference;
    private float soulsValueReference;
    private float levelUpCostReference;
    private float strengthReference;
    private float vitalityReference;
    private float speedReference;
    private float defenseReference;


    private void OnEnable()
    {
        GetStatReferences();
        ShowValuesForStatWindow();
    }

    private void GetStatReferences()
    {

        levelReference = PlayerStats.Level;
        soulsValueReference = PlayerStats.SoulsValue;
        strengthReference = PlayerStats.Strength;
        vitalityReference = PlayerStats.Vitality;
        speedReference = PlayerStats.Speed;
        defenseReference = PlayerStats.Defense;
    }
    private void ShowValuesForStatWindow()
    {
        levelUpCostReference = levelReference * 50;

        Attributes[0].text = PlayerStats.PlayerName;
        Attributes[1].text = "Level  " + levelReference.ToString();
        Attributes[2].text = "Souls  " + soulsValueReference.ToString();
        Attributes[3].text = "LevelUp  " + levelUpCostReference.ToString();
        Attributes[4].text = "STR  " + strengthReference.ToString();
        Attributes[5].text = "VIT  " + vitalityReference.ToString();
        Attributes[6].text = "SPD  " + speedReference.ToString();
        Attributes[7].text = "DEF  " + defenseReference.ToString();

    }

    private void IncreaseStat(int _index)
    {
        if (soulsValueReference < levelUpCostReference)
        { return; }
        switch (_index)
        {
            case 0:
                strengthReference++;
                levelReference++;
                break;
            case 1:
                vitalityReference++;
                levelReference++;
                break;
            case 2:
                speedReference++;
                levelReference++;
                break;
            case 3:
                defenseReference++;
                levelReference++;
                break;
            default:
                break;
        }
        soulsValueReference -= levelUpCostReference;

        ShowValuesForStatWindow();

    }


    private void DecreaseStat(int _index)
    {
        switch (_index)
        {
            case 0:
                if (strengthReference > PlayerStats.Strength)
                {
                    strengthReference--;
                    levelReference--;
                    levelUpCostReference = levelReference * 50;
                    soulsValueReference += levelUpCostReference;
                }
                break;
            case 1:
                if (vitalityReference > PlayerStats.Vitality)
                {
                    vitalityReference--;
                    levelReference--;
                    levelUpCostReference = levelReference * 50;
                    soulsValueReference += levelUpCostReference;
                }
                break;
            case 2:
                if (speedReference > PlayerStats.Speed)
                {
                    speedReference--;
                    levelReference--;
                    levelUpCostReference = levelReference * 50;
                    soulsValueReference += levelUpCostReference;
                }
                break;
            case 3:
                if (defenseReference > PlayerStats.Defense)
                {
                    defenseReference--;
                    levelReference--;
                    levelUpCostReference = levelReference * 50;
                    soulsValueReference += levelUpCostReference;
                }
                break;
            default:
                break;
        }

        ShowValuesForStatWindow();
    }


    public void SaveChanges()
    {
        PlayerStats.Level = levelReference;
        PlayerStats.SoulsValue = soulsValueReference;
        PlayerStats.Strength = strengthReference;
        PlayerStats.Vitality = vitalityReference;
        PlayerStats.Speed = speedReference;
        PlayerStats.Defense = defenseReference;

        SoulsSystem.instance.LevelUpCost = PlayerStats.Level * 50;
        SoulsSystem.instance.UpdateSoulsCounter();

        PlayerHealthScript.UpdateMaxHealth();
        PlayerHealthScript.ResetHealth();

        PlayerStamina.UpdateMaxStamina();

    }
}
