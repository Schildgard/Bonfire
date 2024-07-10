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
        vegetationSpawnPositions.Clear();

        planePositions = planeMesh.vertices;

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
        int vertexCount = vegetationSpawnPositions.Count * 4;

        int triangleIndexCount = 3 * 2 * vertexCount; // vermutlich Error

        Vector3[] verts = new Vector3[vertexCount];
        int[] triangles = new int[triangleIndexCount]; // vermutlich Error

        foreach (var position in vegetationSpawnPositions)
        {
            //set verticies of quad
        }
        int vertindex = 0;
        int triIndex = 0;
        for (int i = 0; i < vegetationSpawnPositions.Count; i++)
        {
            verts[vertindex] = vegetationSpawnPositions[i];
            verts[vertindex + 1] = vegetationSpawnPositions[i] + Vector3.right;
            verts[vertindex + 2] = vegetationSpawnPositions[i] + Vector3.up;
            verts[vertindex + 3] = vegetationSpawnPositions[i]+Vector3.up+Vector3.right;
            vertindex += 4;

         //  triangles[triIndex] = i;
         //  triangles[triIndex + 1] = i +
        }

    }
}
