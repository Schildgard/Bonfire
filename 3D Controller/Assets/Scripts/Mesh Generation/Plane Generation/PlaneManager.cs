using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PlaneManager : MonoBehaviour
{
    private PlaneGenerator planeGenerator;

    [SerializeField] private ShapeSettings shapeSettings;
    [SerializeField] private Material planeMaterial;

    [SerializeField, Range(4, 240)] private int resolution;

    private MeshFace plane;
    private NoiseFilter noiseFilter;

    public ShapeSettings ShapeSettings => shapeSettings;
    private Vector3 SpawnOffset;

    public void CreatePlane()
    {
        noiseFilter = new NoiseFilter(shapeSettings);
        GeneratePlanes();
    }


    private void GeneratePlanes()
    {
        planeGenerator = new PlaneGenerator(planeMaterial, noiseFilter, resolution);

        if (plane == null)
        {
            plane = planeGenerator.CreatePlaneItem();
            Debug.Log("Plane was created");


        }

    }

    public void UpdatePlaneMesh()
    {
        if (plane == null) { return; }
        planeGenerator.UpdatePlaneMesh(plane, resolution);
    }
}
