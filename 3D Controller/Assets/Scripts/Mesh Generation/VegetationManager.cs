using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationManager : MonoBehaviour
{
    private VegetationGenerator vegGenerator;
    private InstancedMesh_VegetationGenerator instancedVegGenerator;
    [SerializeField] private Material vegetationMaterial;
    [SerializeField] private Material GPUInstancedMaterial;
    private MeshFace vegetationMesh;
    private Mesh planeMesh;

    [SerializeField] private float threshold;

    [SerializeField] GameObject PositionIndicatorPrefab;


    MeshFace[] instancedMeshes;


    public bool generateInstancedMeshes;
    private void Start()
    {
        planeMesh = GameObject.Find("Custom Plane").GetComponent<MeshFilter>().mesh;

        instancedVegGenerator = new InstancedMesh_VegetationGenerator(planeMesh, GPUInstancedMaterial, threshold);

        vegGenerator = new VegetationGenerator(planeMesh, vegetationMaterial, threshold);


        GenerateVegetations();

    }

    private void GenerateVegetations()
    {
        if (generateInstancedMeshes)
        {
        instancedVegGenerator.GenerateVegetationItem();
            return;
        }

        vegetationMesh = vegGenerator.GenerateVegetationItem();

    }

    private void ShowSpawnPositions(List<Vector3> _positions)
    {
        foreach (var position in _positions)
        {
            Instantiate(PositionIndicatorPrefab, position, Quaternion.identity);
        }

    }

}
