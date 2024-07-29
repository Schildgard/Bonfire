using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{

    private Light Lightsource;
    private float timer;
    private float flickerIntervall;
    [SerializeField]private float minInterval;
    [SerializeField]private float maxInterval;

    [SerializeField] private float intensityModifier;

    // Start is called before the first frame update
    void Start()
    {
        Lightsource = GetComponent<Light>();
        flickerIntervall = Random.Range(minInterval, maxInterval);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > flickerIntervall)
        {
            //flick
            Lightsource.intensity += intensityModifier;


            //reset timer
            flickerIntervall = Random.Range(minInterval, maxInterval);
            timer = 0;
            intensityModifier *= -1;
        }
        
    }
}
