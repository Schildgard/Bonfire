using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CustomPlane
{

    public GameObject PlaneObject;
    [SerializeField]private ShapeSettings shapeSettings;

    public Material planeMaterial;
    [SerializeField, Range(4, 240)] public int resolution;
    public ShapeSettings ShapeSettings => shapeSettings;

    public NoiseFilter noiseFilter;

    public PlaneGenerator PlaneGenerator;
}
