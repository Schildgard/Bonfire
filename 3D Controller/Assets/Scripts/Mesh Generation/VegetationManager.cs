using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationManager : MonoBehaviour
{
    private VegetationGenerator vegGenerator;
    private MeshFace vegetationMesh;
    private Mesh planeMesh;

    [SerializeField] private Material vegetationMaterial;
    [SerializeField] private float threshold;

    #region GPU Instancing
    private InstancedMesh_VegetationGenerator instancedVegGenerator;
    [SerializeField] private Material GPUInstancedMaterial;
    Mesh instancedMesh;

    Matrix4x4[] Batches;
    List<List<Matrix4x4>> ListofBatchLists;
    int batchCount;


    #endregion


    #region Testing Variables
    [SerializeField] GameObject PositionIndicatorPrefab;
    public bool generateInstancedMeshes;
    #endregion

    private void Start()
    {
        planeMesh = GameObject.Find("Custom Plane").GetComponent<MeshFilter>().mesh;
        InitializeGenerators();

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
        instancedVegGenerator = new InstancedMesh_VegetationGenerator(planeMesh, GPUInstancedMaterial, threshold);

        vegGenerator = new VegetationGenerator(planeMesh, vegetationMaterial, threshold);
    }

    private void GenerateVegetations()
    {
        if (generateInstancedMeshes)
        {
            CreateInstancedVegetations();
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


    private void CreateInstancedVegetations()
    {
        List<Vector3> spawnPositions = instancedVegGenerator.CalculateSpawnPositions(planeMesh);
        ListofBatchLists = new List<List<Matrix4x4>>();

        instancedMesh = instancedVegGenerator.GenerateMesh();

        int ListIndex = 0;
        int counter = 0;
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (counter >= 1000)
            {
                ListIndex++;
                counter = 0;
            }

            ListofBatchLists[ListIndex].Add(Matrix4x4.TRS(spawnPositions[i], Quaternion.identity, Vector3.one));
            counter++;
        }


        batchCount = spawnPositions.Count;
        Batches = new Matrix4x4[batchCount];

        for (int i = 0; i < batchCount; i++)
        {
            Batches[i] = Matrix4x4.TRS(spawnPositions[i], Quaternion.identity, Vector3.one);
        }


    }

    private void RenderBatches()
    {

        Graphics.DrawMeshInstanced(instancedMesh, 0, GPUInstancedMaterial, Batches);

        Debug.Log("Completed a Render Cycle");
    }
}
