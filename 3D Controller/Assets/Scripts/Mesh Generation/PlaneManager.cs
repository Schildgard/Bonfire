using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    private PlaneGenerator planeGenerator;

    private NoiseFilter noiseFilter;
    [SerializeField] private ShapeSettings shapeSettings;



    [SerializeField] private Material planeMaterial;
    [SerializeField] private int resolution;
    [SerializeField] private int size;

    private MeshFace plane;


    private void Awake()
    {
        noiseFilter = new NoiseFilter(shapeSettings);
        GeneratePlanes();
    }
    private void GeneratePlanes()
    {
        planeGenerator = new PlaneGenerator(planeMaterial, noiseFilter, resolution, size);
        plane = planeGenerator.CreatePlaneItem();
    }
}
