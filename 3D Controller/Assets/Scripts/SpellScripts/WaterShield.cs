using UnityEngine;

public class WaterShield : MonoBehaviour, IDamageable, IElectrilizable
{
    private StatScript Stats;
    private float previousDefValue;
    [SerializeField] private float buffedDefValue;
    [SerializeField] private GameObject SplashEffect;

    [SerializeField] private GameObject ElectrifyEffect;


    // Wetnesses AOE Attacking Enemies
    // Gets Electrified when Contact with Lightning
    //Deals Damage to Attacking Enemy when Electrified




    private void Awake()
    {
        Stats = GetComponentInParent<StatScript>();
    }



    private void Start()
    {
        BoostDefense();
    }


    private void BoostDefense()
    {
        previousDefValue = Stats.Defense;
        Stats.Defense = buffedDefValue;
    }

    private void ResetDefense()
    {
        Stats.Defense = previousDefValue;
    }
    public void Electrify()
    {
        ElectrifyEffect.SetActive(true);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }


    public void GetDamage(float _damage)
    {
        Die();
    }

    private void OnDestroy()
    {
        ResetDefense();
        //if not Electrified
        Instantiate(SplashEffect, transform.root);
        //if Electrified
        //Instantiate Electrified Splash Effect

    }
}
