using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    [SerializeField] private Light Light;


    private float timer;
    private float startLightIntensity;
   [SerializeField]private float flickerIntensity;
   [SerializeField]private float flickerPerSecond;
   [SerializeField]private float speedRandomness;




    // Start is called before the first frame update
    void Start()
    {
        Light = GetComponent<Light>();
        startLightIntensity = Light.intensity;


    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * (1 - Random.Range(-speedRandomness, speedRandomness)) * Mathf.PI;
        Light.intensity = startLightIntensity + Mathf.Sin(timer * flickerPerSecond) * flickerIntensity;

    }
}
