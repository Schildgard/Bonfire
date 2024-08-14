using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnvironmentManager : MonoBehaviour
{

    private AreaCollection areaCollection;
    private Area[] areas;
    private Dictionary<EnvironmentalVegetation, Mesh> instancedEnvironment;


    private void Awake()
    {
        areaCollection = GetComponent<AreaCollection>();
    }
    public void Initialize()
    {
        // Initialize assigns each area their own Mesh Reference, because when iterating through the areas in a 'foreachloop', I can be sure that the correct Mesh is
        //transmitted for each generator. This could probably also be handled with a Mesh Array or List in this Manager script, but it would be more difficult to locate errors, since
        //this Manager handles no visibility.

        RemoveEnvironmentPrefabs();
        areas = areaCollection.Areas;
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
        //GPU Instancing
        //This script needs to renders all GPU Instanced Vegetations for this, it receives the RenderData from all the Instanced_Mesh Generators and saves them in a Dictionary.

        instancedEnvironment = new Dictionary<EnvironmentalVegetation, Mesh>();

        foreach (var area in areas)
        {
            foreach (var environment in area.renderableEnvironment)
            {
                instancedEnvironment.Add(environment, environment.EnvironmentGenerator.CreateEnvironmentalMesh());
            }
        }


        //Prefab Generation
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

        //Deletes the Environmental Prefabs and Clears the List. It is calles in UpdateEnvironment so it doesnt stack more and more prefabs whith each call.
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
        //Prefabs are removed from the List but remain in Scene until deleted manually in Hierarchy.
        //The point is to make them no longer affected by UpdateEnvironment
        areaCollection.PrefabsInScene.Clear();
    }
}
