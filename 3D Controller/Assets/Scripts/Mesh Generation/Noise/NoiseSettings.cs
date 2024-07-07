using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NoiseSettings
{

    [Range(1, 8)]
    public int LayerCount; //Noise Position gets evaluated for each layer which makes the more detailed

    public float BaseRoughness; //Smoothens the Noise
    public float Roughness;

    public float Strength; // increases the Noise frequency
    public float Persistence;

    public Vector3 NoiseCenter;

    public float GroundLevel;


}
