using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationManager : MonoBehaviour
{
    private VegetationGenerator[] vegGenerators;

    private Material[] vegetationMaterials;

    private Mesh plane;
    private MeshFace[] vegetationMesh;

    private void Start()
    {
        GenerateAllVegetations();
    }

    private void GenerateAllVegetations()
    {
        vegetationMesh = new MeshFace[vegGenerators.Length];

        for (int i = 0; i < vegGenerators.Length; i++)
        {
            vegetationMesh[i] = vegGenerators[i].GenerateVegetationItem();
        }
    }
}
