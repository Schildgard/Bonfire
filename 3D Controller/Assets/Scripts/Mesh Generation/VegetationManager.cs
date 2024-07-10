using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationManager : MonoBehaviour
{
    private VegetationGenerator vegGenerator;

    private Material vegetationMaterial;

    private MeshFace vegetationMesh;

    private Mesh planeMesh;

    private void Start()
    {
        planeMesh = GameObject.Find("Custom Plane").GetComponent<MeshFilter>().mesh;
        GenerateAllVegetations();
    }

    private void GenerateAllVegetations()
    {
        vegGenerator = new VegetationGenerator(planeMesh, vegetationMaterial);
        vegetationMesh = vegGenerator.GenerateVegetationItem();

    }
}
