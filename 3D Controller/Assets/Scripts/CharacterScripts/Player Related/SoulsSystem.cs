using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulsSystem : MonoBehaviour
{
    
    public static SoulsSystem instance;

   private void Awake()
   {
      if (instance == null) instance = this;
       else { Destroy(gameObject); }
       DontDestroyOnLoad(gameObject);
   }

    
    [SerializeField] private TMP_Text SoulsCounter;
    [SerializeField] private float lostSouls;
    [SerializeField] private StatScript PlayerStats;
    [SerializeField] private float levelUpCost;

    public float LostSouls{ get{ return lostSouls;} set { lostSouls = value;} }
    public float LevelUpCost { get { return levelUpCost; } set { levelUpCost = value; } }

    public void GainSouls(float _soulsValue) 
    {
        PlayerStats.SoulsValue += _soulsValue;
        UpdateSoulsCounter();
    }

    public void UpdateSoulsCounter() 
    {
        SoulsCounter.text = PlayerStats.SoulsValue.ToString();
    }

    public void TransferSouls() 
    {
        lostSouls = PlayerStats.SoulsValue;
        PlayerStats.SoulsValue = 0;
    }
}
