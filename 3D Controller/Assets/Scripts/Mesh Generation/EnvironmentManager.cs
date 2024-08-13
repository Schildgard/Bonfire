using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Area[] areas;
    [SerializeField] private List<GameObject> prefabsInScene;

    private Dictionary<RenderableVegetation, Mesh> instancedEnvironment;

    private bool renderInstancedMeshes; // The point of this bool is to disable the rendering when 'Remove Environment' Button is pressed
                                        // and optimize performance while editing other stuff in Editor


    public void Initialize()
    {
        RemoveEnvironmentPrefabs();

        foreach (var area in areas)
        {
            area.planeMesh = area.areaPlane.GetComponent<MeshFilter>().sharedMesh;
        }


        InitializeGenerators();
        GenerateVegetations();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (renderInstancedMeshes)
        {
            RenderAllInstancedMeshes();
        }
    }



    private void InitializeGenerators()
    {
        foreach (var area in areas)
        {
            foreach (var environment in area.renderableEnvironment)
            {
                environment.InitializeGenerator(area.planeMesh, area.areaPlane.transform);
            }
            foreach (var environment in area.spawnableEnvironment)
            {
                environment.InitializeGenerator(area.planeMesh, area.areaPlane.transform);
            }
        }
    }

    private void GenerateVegetations()
    {
        instancedEnvironment = new Dictionary<RenderableVegetation, Mesh>();

        foreach (var area in areas)
        {



            foreach (var environment in area.renderableEnvironment)
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
        }


        prefabsInScene = new List<GameObject>();

        foreach (var area in areas)
        {


            foreach (var environment in area.spawnableEnvironment)
            {

                environment.EnvironmentGenerator.SetSpawnPositions();

                if (environment.RandomRotation)
                {
                    foreach (var position in environment.EnvironmentGenerator.SpawnPositions)
                    {

                        prefabsInScene.Add(Instantiate(environment.Prefab, position, Quaternion.Euler(0, Random.Range(0, 360), 0)));
                    }
                }
                else
                {
                    foreach (var position in environment.EnvironmentGenerator.SpawnPositions)
                    {
                        prefabsInScene.Add(Instantiate(environment.Prefab, position, Quaternion.identity));
                    }
                }
            }

        }
        renderInstancedMeshes = true;
    }



    private void RenderInstancedMesh(Mesh _mesh, Material _material, List<List<Matrix4x4>> _matrixLists)
    {

        foreach (var MatrixList in _matrixLists)
        {
            Graphics.DrawMeshInstanced(_mesh, 0, _material, MatrixList);
        }
    }

    public void RenderAllInstancedMeshes()
    {
        if (instancedEnvironment == null || instancedEnvironment.Count <= 0) { return; }
        foreach (var environment in instancedEnvironment)
        {
            RenderInstancedMesh(environment.Value, environment.Key.Material, environment.Key.EnvironmentGenerator.Matrices);
        }
    }

    public void UpdateEnvironment()
    {
        RemoveEnvironmentPrefabs();

        InitializeGenerators();
        GenerateVegetations();
    }


    public void RemoveEnvironmentPrefabs()
    {
        if (prefabsInScene == null)
        {
            Debug.Log("No PrefabsInScene Reference known");
            return;
        }

        foreach (var prefab in prefabsInScene)
        {
            DestroyImmediate(prefab.gameObject);
        }
        prefabsInScene.Clear();
        renderInstancedMeshes = false;
    }
}
