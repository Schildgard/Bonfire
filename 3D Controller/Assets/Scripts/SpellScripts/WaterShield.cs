using UnityEngine;

public class WaterShield : MonoBehaviour, IDamageable, IElectrilizable
{
    private StatScript Stats;
    private float previousDefValue;
    [SerializeField] private float buffedDefValue;
    [SerializeField] private GameObject SplashEffect;

    [SerializeField] private GameObject ElectrifyEffect;
    [SerializeField] private GameObject SparkEffect;

    [SerializeField] private float scaleIncreaseSpeed;
    [SerializeField] private Transform sphereObject;
    [SerializeField] private GameObject instantiationEffect;
    private float transformScale;
    private bool electrified;


    private void Awake()
    {

        transform.parent = GameObject.Find("Player").transform;
        Stats = GetComponentInParent<StatScript>();

    }


    private void Start()
    {
        BoostDefense();
    }
    private void Update()
    {
        if (transformScale < 2f)
        {
            transformScale = Mathf.Clamp(transformScale + (Time.deltaTime * scaleIncreaseSpeed), 0f, 2f);
            sphereObject.localScale = new Vector3(transformScale, transformScale, transformScale);
        }
        else if (transformScale >=2 && instantiationEffect.activeSelf)
        {
            instantiationEffect.SetActive(false);
        }
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
        electrified = true;
    }

    public void Electrify(Vector3 _hitpoint)
    {
        Debug.Log("Call of Electrified Method used a Vector3 as Input, which is not intended for this Effect." +
        "Go the Scripit in which the Electrify Method is called and remove the Vector3 Paramater.");
        Electrify();
    }

    public void Die()
    {
       this.gameObject.SetActive(false);
    }


    public void GetDamage(float _damage)
    {
        Die();
    }

    private void OnDestroy()
    {
        ResetDefense();
        if (!electrified)
        {
            GameObject Splash = Instantiate(SplashEffect, transform.root);
            Destroy(Splash, 0.5f);
        }

        else
        {
            GameObject Spark = Instantiate(SparkEffect, transform.position, Quaternion.identity);
            Destroy(Spark, 0.5f);
        }

    }
}
