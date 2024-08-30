using UnityEngine;

public class Flicker : MonoBehaviour
{
    //This script makes torch fire lights flicker.

    private Light Lightsource;
    private float timer;
    private float flickerIntervall;
    [SerializeField]private float minInterval;
    [SerializeField]private float maxInterval;

    [SerializeField] private float intensityModifier;

    void Start()
    {
        Lightsource = GetComponent<Light>();
        flickerIntervall = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > flickerIntervall)
        {
            //when timer reaches Intverfall, intensify the light
            Lightsource.intensity += intensityModifier;

            //reset timer and set light intensity to a random lower value
            flickerIntervall = Random.Range(minInterval, maxInterval);
            timer = 0;
            intensityModifier *= -1;
        }
        
    }
}
