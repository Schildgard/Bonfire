using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    private PlaneGenerator planeGenerator;

    [SerializeField] private ShapeSettings shapeSettings;
    [SerializeField] private Material planeMaterial;

    [SerializeField, Range(1,255)] private int resolution;
    [SerializeField] private int size;

    private MeshFace plane;
    private NoiseFilter noiseFilter;

    public ShapeSettings ShapeSettings => shapeSettings;




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

    public void UpdatePlaneMesh()
    {
        if (plane == null) {  return; }
        planeGenerator.UpdatePlaneMesh(plane, resolution, size);
    }
}
