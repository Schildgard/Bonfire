using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New Shape Settings", menuName = "ShapeSettings")]



public class ShapeSettings : ScriptableObject
{
    public NoiseLayer[] NoiseLayers;
}

[System.Serializable]
public class NoiseLayer
{
    public bool enabled;
    public NoiseSettings NoiseSettings;
}
