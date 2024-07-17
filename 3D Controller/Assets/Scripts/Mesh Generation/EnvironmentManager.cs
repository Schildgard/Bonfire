using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentManager : MonoBehaviour
{
    private Mesh planeMesh;
    private List<GameObject> prefabsInScene;

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
        RenderAllInstancedMeshes();
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


        prefabsInScene = new List<GameObject>();
        foreach (var environment in spawnableEnvironment)
        {

            environment.EnvironmentGenerator.SetSpawnPositions();
            int index = 0;


            if (environment.RandomRotation)
            {
                foreach (var position in environment.EnvironmentGenerator.SpawnPositions)
                {

                    prefabsInScene.Add(Instantiate(environment.Prefab, position, Quaternion.Euler(0, Random.Range(0, 360), 0)));
                    index++;
                }
            }
            else
            {
                foreach (var position in environment.EnvironmentGenerator.SpawnPositions)
                {
                    prefabsInScene.Add(Instantiate(environment.Prefab, position, Quaternion.identity));
                    index++;
                }
            }
        }
    }



    private void RenderInstancedMeshes(Mesh _mesh, Material _material, List<List<Matrix4x4>> _matrixLists)
    {

        foreach (var MatrixList in _matrixLists)
        {
            Graphics.DrawMeshInstanced(_mesh, 0, _material, MatrixList);
        }
    }

    public void RenderAllInstancedMeshes()
    {
        if (instancedEnvironment.Count <= 0) { return; }
        foreach (var environment in instancedEnvironment)
        {
            RenderInstancedMeshes(environment.Value, environment.Key.Material, environment.Key.EnvironmentGenerator.Matrices);

            //Änderung : Die Matrizen zwischenspeichern, oder Unity sagen, dass da aufjedenfall ne Matrix drin ist.
        }
    }

    public void UpdateEnvironment()
    {

        foreach (var prefab in prefabsInScene)
        {
            Destroy(prefab.gameObject);
        }
        prefabsInScene.Clear();

        InitializeGenerators();
        GenerateVegetations();
    }
}
