using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnvironmentManager : MonoBehaviour
{

   // public EnvironmentManager(AreaCollection _areaCollection)
   // {
   //     areaCollection = _areaCollection;
   //     areas = areaCollection.Areas;
   //
   // }
    private AreaCollection areaCollection;
    private Area[] areas;
    private Dictionary<RenderableVegetation, Mesh> instancedEnvironment;


    private void Awake()
    {
        areaCollection = GetComponent<AreaCollection>();
        areas = areaCollection.Areas;
    }
    public void Initialize()
    {
        RemoveEnvironmentPrefabs();

        foreach (var area in areas)
        {
            area.planeMesh = area.areaPlane.GetComponent<MeshFilter>().sharedMesh;
        }


        InitializeGenerators();
        GenerateEnvironment();
    }

    private void Update()
    {
        if (areaCollection.RenderInstancedMeshes)
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

    private void GenerateEnvironment()
    {
        instancedEnvironment = new Dictionary<RenderableVegetation, Mesh>();

        foreach (var area in areas)
        {
            foreach (var environment in area.renderableEnvironment)
            {
                instancedEnvironment.Add(environment, environment.EnvironmentGenerator.CreateEnvironmentalMesh());
            }
        }

        //  prefabsInScene = new List<GameObject>();

        foreach (var area in areas)
        {
            foreach (var environment in area.spawnableEnvironment)
            {
                environment.EnvironmentGenerator.SetSpawnPositions();

                if (environment.RandomRotation)
                {
                    foreach (var position in environment.EnvironmentGenerator.SpawnPositions)
                    {
                        areaCollection.PrefabsInScene.Add(Instantiate(environment.Prefab, position, Quaternion.Euler(0, Random.Range(0, 360), 0)));
                    }
                }
                else
                {
                    foreach (var position in environment.EnvironmentGenerator.SpawnPositions)
                    {
                        areaCollection.PrefabsInScene.Add(Instantiate(environment.Prefab, position, Quaternion.Euler(0, Random.Range(0, 360), 0)));
                    }
                }
            }

        }
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
        GenerateEnvironment();
    }


    public void RemoveEnvironmentPrefabs()
    {
        if (areaCollection.PrefabsInScene == null)
        {
            Debug.Log("No PrefabsInScene Reference known");
            return;
        }

        foreach (var prefab in areaCollection.PrefabsInScene)
        {
            DestroyImmediate(prefab.gameObject);
        }
        areaCollection.PrefabsInScene.Clear();
    }

    public void ClearPrefabList()
    {
        areaCollection.PrefabsInScene.Clear();
    }
}
