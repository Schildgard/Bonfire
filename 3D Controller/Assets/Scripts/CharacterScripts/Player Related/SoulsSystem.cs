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


    private TMP_Text soulsCounter;
    [SerializeField] private float lostSouls;
    private StatScript playerStats;
    [SerializeField] private float levelUpCost;


    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<StatScript>();
        soulsCounter = GetComponentInChildren<TMP_Text>();
    }

    public float LostSouls { get { return lostSouls; } set { lostSouls = value; } }
    public float LevelUpCost { get { return levelUpCost; } set { levelUpCost = value; } }

    public void GainSouls(float _soulsValue)
    {
        playerStats.SoulsValue += _soulsValue;
        UpdateSoulsCounter();
    }

    public void UpdateSoulsCounter()
    {
        soulsCounter.text = playerStats.SoulsValue.ToString();
    }

    public void TransferSouls()
    {
        lostSouls = playerStats.SoulsValue;
        playerStats.SoulsValue = 0;
        UpdateSoulsCounter();
    }
}
