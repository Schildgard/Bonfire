using UnityEngine;

public class SoulsCrate : MonoBehaviour
{
    //A Crate is spawned every time the player dies. It contains the souls the player had when dying and can be retrieved.
    public float soulsValue;
    private bool activated;
    [SerializeField] private GameObject tooltipCanvas;


    private void Start()
    {
        soulsValue = SoulsSystem.instance.LostSouls;
        SoulsSystem.instance.LostSouls = 0;
    }

    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SoulsSystem.instance.GainSouls(soulsValue);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            bool isAlive = _other.gameObject.GetComponent<HealthScript>().isAlive;
            if (isAlive)
            {
                tooltipCanvas.SetActive(true);
                activated = true;
            }
        }
    }


    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            tooltipCanvas.SetActive(false);
            activated = false;
        }
    }

}
