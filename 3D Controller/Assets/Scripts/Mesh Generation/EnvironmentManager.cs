using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentManager : MonoBehaviour
{
    private Mesh planeMesh;
    private GameObject[] prefabsInScene;

    [SerializeField] private List<RenderableVegetation> renderableEnvironment;
    [SerializeField] private List<RenderablePrefabs> spawnableEnvironment;

    private Dictionary<RenderableVegetation, Mesh> instancedEnvironment;

    private void Start()
    {
        planeMesh = GameObject.Find("Custom Plane").GetComponent<MeshFilter>().mesh;
        InitializeGenerators();
        GenerateVegetations();
    }


    private void Update()
    {
        RenderInstancedMeshes();
    }



    private void InitializeGenerators()
    {
        foreach (var environment in renderableEnvironment)
        {
            environment.InitializeGenerator(planeMesh);
        }

        foreach (var environment in spawnableEnvironment)
        {
            environment.InitializeGenerator(planeMesh);
        }
    }

    private void GenerateVegetations()
    {
        instancedEnvironment = new Dictionary<RenderableVegetation, Mesh>();
        foreach (var environment in renderableEnvironment)
        {
            if (environment.RenderMode == 0)
            {
                environment.EnvironmentGenerator.CreateEnvironmentalMesh();
            }

            if (environment.RenderMode == 1)
            {
                instancedEnvironment.Add(environment, environment.EnvironmentGenerator.CreateEnvironmentalMesh());
            }
        }


        foreach (var environment in spawnableEnvironment)
        {

            environment.EnvironmentGenerator.SetSpawnPositions();
            prefabsInScene = new GameObject[environment.EnvironmentGenerator.SpawnPositions.Count];
            int index = 0;


            if (environment.RandomRotation)
            {
                foreach (var position in environment.EnvironmentGenerator.SpawnPositions)
                {

                    prefabsInScene[index] = Instantiate(environment.Prefab, position, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    index++;
                }
            }
            else
            {
                foreach (var position in environment.EnvironmentGenerator.SpawnPositions)
                {
                    prefabsInScene[index] = Instantiate(environment.Prefab, position, Quaternion.identity);
                    index++;
                }
            }
        }
    }



    private void RenderBatches(Mesh _mesh, Material _material, List<List<Matrix4x4>> _matrixLists)
    {

        foreach (var MatrixList in _matrixLists)
        {
            Graphics.DrawMeshInstanced(_mesh, 0, _material, MatrixList);
        }
    }

    public void RenderInstancedMeshes()
    {
        if (instancedEnvironment.Count <= 0) { return; }
        foreach (var environment in instancedEnvironment)
        {
            RenderBatches(environment.Value, environment.Key.Material, environment.Key.EnvironmentGenerator.Matrices);

            //Änderung : Die Matrizen zwischenspeichern, oder Unity sagen, dass da aufjedenfall ne Matrix drin ist.
        }
    }

    public void UpdateEnvironment()
    {
        Debug.Log("Change");

        foreach (var prefab in prefabsInScene)
        {
            Debug.Log("try to destroy: " + prefab.name);
            Destroy(prefab.gameObject);
        }

        InitializeGenerators();
        GenerateVegetations();
    }
}
