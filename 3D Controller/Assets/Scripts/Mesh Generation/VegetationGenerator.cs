using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationGenerator
{

    public VegetationGenerator(Mesh _mesh, Material _material)
    {
        planeMesh = _mesh;
        material = _material;
    }


    private Mesh planeMesh;
    private Noise noise;
    private Material material;
    private Vector3[] planePositions;


    Vector3[] vegetationVerticies;


    List<Vector3> vegetationSpawnPositions = new List<Vector3>();
    private float spawnThreshold;



    public MeshFace GenerateVegetationItem()
    {

        GameObject VegetationPlaneObject = new GameObject();

        MeshFilter meshFilter = VegetationPlaneObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = VegetationPlaneObject.AddComponent<MeshRenderer>();
        Mesh vegetationMesh = new Mesh();

        vegetationMesh.name = "Vegetation";
        meshRenderer.sharedMaterial = material;
        meshFilter.mesh = vegetationMesh;

        DrawVegetation(vegetationMesh);


        return new MeshFace(meshRenderer, meshFilter);
    }




    private void CalculateSpawnPositions()
    {
        planePositions = planeMesh.vertices;

        vegetationSpawnPositions.Clear();

        float spawnValue;
        //Evaluate Positions in Noise so it returns a Value between 0 and 1

        foreach (var position in planePositions)
        {
            spawnValue = noise.Evaluate(position);
            if (spawnValue > spawnThreshold)
            {
                vegetationSpawnPositions.Add(position);
            }
        }
    }

    private void DrawVegetation(Mesh _mesh)
    {


    }
}
