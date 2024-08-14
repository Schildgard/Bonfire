using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NoiseSettings
{

    [Range(1, 8)]
    public int LayerCount; //Noise Position gets evaluated for each layer which makes the more detailed

    public float BaseRoughness; //Zieht das Noise zusammen/auseinander  // Zoom in das Noise
    public float Roughness;

    public float Strength; // increases the Noise frequency
    public float Persistence; // Mehr / Weniger Noise

    public Vector3 NoiseCenter;

    public float GroundLevel;


}
