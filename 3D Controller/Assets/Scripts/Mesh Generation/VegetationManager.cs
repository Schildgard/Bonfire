using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationManager : MonoBehaviour
{
    private VegetationGenerator vegGenerator;
    private MeshFace vegetationMesh;
    private Mesh planeMesh;

    [SerializeField] private Material vegetationMaterial;

    #region GPU Instancing
    private InstancedMesh_VegetationGenerator instancedVegGenerator;
    [SerializeField] private Material GPUInstancedMaterial;
    Mesh instancedMesh;

    List<List<Matrix4x4>> ListofMatrixLists;

    #endregion


    #region Testing Variables
    [SerializeField] GameObject PositionIndicatorPrefab;
    public bool generateInstancedMeshes;
    public bool generatePrefabs;

    List<Vector3> spawnPositions;
    #endregion


    [SerializeField] private EnvironmentalSettingsLayer environmentalSettings;

    private void Start()
    {
        planeMesh = GameObject.Find("Custom Plane").GetComponent<MeshFilter>().mesh;

        InitializeGenerators();
        spawnPositions = instancedVegGenerator.CalculateSpawnPositions(planeMesh);

        GenerateVegetations();


    }


    private void Update()
    {
        if (generateInstancedMeshes)
        {
            RenderBatches();
        }       
    }



    private void InitializeGenerators()
    {
        instancedVegGenerator = new InstancedMesh_VegetationGenerator(planeMesh, GPUInstancedMaterial, environmentalSettings.EnvironmentalSettings);

        vegGenerator = new VegetationGenerator(planeMesh, vegetationMaterial, environmentalSettings.EnvironmentalSettings);

    }

    private void GenerateVegetations()
    {
        if (generateInstancedMeshes)
        {
            CreateInstancedVegetations();
            return;
        }

        else if (generatePrefabs)
        {
            GeneratePrefabs(spawnPositions);
            return;
        }

        vegetationMesh = vegGenerator.GenerateVegetationItem();

    }

    private void GeneratePrefabs(List<Vector3> _positions)
    {
        foreach (var position in _positions)
        {
            Instantiate(PositionIndicatorPrefab, position, Quaternion.identity);
        }

    }

    private void CreateInstancedVegetations()
    {
        List<Vector3> spawnPositions = instancedVegGenerator.CalculateSpawnPositions(planeMesh);
        Debug.Log($"Spawn Positions has {spawnPositions.Count} positions");
        ListofMatrixLists = new List<List<Matrix4x4>>();
        ListofMatrixLists.Add(new List<Matrix4x4>());
        Debug.Log($"Lists of Matrix Lists ha {ListofMatrixLists.Count} Lists in it");

        instancedMesh = instancedVegGenerator.GenerateMesh();

        int ListIndex = 0;
        int counter = 0;
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (counter >= 1000)
            {
                ListofMatrixLists.Add(new List<Matrix4x4>());
                Debug.Log($"Lists of Matrizes added a new List to it and now consists of {ListofMatrixLists.Count} Lists");
                ListIndex++;

                counter = 0;
            }

            ListofMatrixLists[ListIndex].Add(Matrix4x4.TRS(spawnPositions[i]+ instancedVegGenerator.EnvironmentalSettings.Offset, Quaternion.identity, instancedVegGenerator.EnvironmentalSettings.ScaleMultiplier));
            counter++;
        }
    }

    private void RenderBatches()
    {
        foreach (var MatrixList in ListofMatrixLists)
        {
            Graphics.DrawMeshInstanced(instancedMesh, 0, GPUInstancedMaterial, MatrixList);
        }

    }

}
