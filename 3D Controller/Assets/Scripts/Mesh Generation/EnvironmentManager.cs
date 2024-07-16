using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private Mesh planeMesh;

    [SerializeField] private List<RenderableVegetation> renderableEnvironment;
    [SerializeField] private List<RenderablePrefabs> spawnableEnvironment;

    private Dictionary<RenderableVegetation, Mesh> instancedEnvironment = new Dictionary<RenderableVegetation, Mesh>();

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
            Debug.Log($"Initialized Generator with Material {environment.Material} ");
            environment.InitializeGenerator(planeMesh);
        }

        foreach (var environment in spawnableEnvironment)
        {
            environment.InitializeGenerator(planeMesh);
        }
    }

    private void GenerateVegetations()
    {

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

        foreach(var environment in spawnableEnvironment)
        {
            environment.EnvironmentGenerator.SetSpawnPositions();
            
            foreach (var position in environment.EnvironmentGenerator.SpawnPositions)
            {
                Instantiate(environment.Prefab, position, Quaternion.identity);
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


}
