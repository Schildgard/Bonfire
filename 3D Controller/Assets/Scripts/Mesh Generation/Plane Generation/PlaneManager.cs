using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PlaneManager : MonoBehaviour
{
    [SerializeField] private List<CustomPlane> generatedPlanes;
    [SerializeField] private ShapeSettings shapeSettings;

    public ShapeSettings ShapeSettings => shapeSettings;


    public void CreatePlane()
    {
        GeneratePlanes();
    }


    private void GeneratePlanes()
    {
        foreach (var item in generatedPlanes)
        {
            if (item.PlaneObject != null) { continue; }
            item.PlaneGenerator = new PlaneGenerator(item.planeMaterial, new NoiseFilter(item.ShapeSettings), item.resolution);
            item.PlaneObject = item.PlaneGenerator.CreatePlaneItem();
        }
    }

    public void UpdatePlaneMesh()
    {
        foreach (var item in generatedPlanes)
        {
            if (item.PlaneObject == null) { return; }
            item.PlaneGenerator.UpdatePlaneMesh(item.PlaneObject, item.resolution);
        }
    }
}
