using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationManager : MonoBehaviour
{
    // private VegetationGenerator vegGenerator;
    private Mesh planeMesh;

    // [SerializeField] private Material vegetationMaterial;
    // [SerializeField] private Material spawnObjectsMaterial;
    // [SerializeField] private Material GPUInstancingMaterial;

    #region GPU Instancing
    private InstancedMesh_VegetationGenerator instancedVegGenerator;
    //  Mesh instancedMesh;

    List<List<Matrix4x4>> ListofMatrixLists;

    #endregion


    #region Testing Variables



    // [SerializeField] GameObject VegetationPrefab;
    // public bool generateInstancedMeshes;
    // public bool generatePrefabs;

    List<Vector3> spawnPositions;
    #endregion


    // [SerializeField] private EnvironmentalSettingsLayer environmentalSettings;


    [SerializeField] private List<RenderableVegetation> renderableVegetations;

    private Dictionary<RenderableVegetation, Mesh> instancedEnvironment = new Dictionary<RenderableVegetation, Mesh>();

    private void Start()
    {
        planeMesh = GameObject.Find("Custom Plane").GetComponent<MeshFilter>().mesh;

        InitializeGenerators();

        GenerateVegetations();


    }


    private void Update()
    {
        if (instancedEnvironment.Count <= 0) { return; }
        foreach (var environment in instancedEnvironment)
        {
            RenderBatches(environment.Value, environment.Key.Material, environment.Key.EnvironmentGenerator.Matrices);

            //Änderung : Die Matrizen zwischenspeichern, oder Unity sagen, dass da aufjedenfall ne Matrix drin ist.
        }
    }



    private void InitializeGenerators()
    {

        foreach (var environment in renderableVegetations)
        {
            environment.InitializeGenerator(planeMesh);
        }

    }

    private void GenerateVegetations()
    {

        foreach (var environment in renderableVegetations)
        {
            if (environment.RenderMode == 1)
            {
                environment.EnvironmentGenerator.CreateEnvironmentalMesh();
            }

            if (environment.RenderMode == 2)
            {
                instancedEnvironment.Add(environment, environment.EnvironmentGenerator.CreateEnvironmentalMesh());
            }
        }

    }

    private void GeneratePrefabs(List<Vector3> _positions)
    {
        foreach (var position in _positions)
        {
            // Instantiate(VegetationPrefab, position, Quaternion.identity);
        }

    }



    private void RenderBatches(Mesh _mesh, Material _material, List<List<Matrix4x4>> _matrixLists)
    {

        foreach (var MatrixList in _matrixLists)
        {
            Graphics.DrawMeshInstanced(_mesh, 0, _material, MatrixList);
        }
    }


}
