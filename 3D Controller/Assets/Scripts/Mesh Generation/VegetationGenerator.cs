using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationGenerator
{

    public VegetationGenerator(Mesh _mesh, Material _material, float _threshold)
    {
        planeMesh = _mesh;
        material = _material;
        noise = new Noise();
        spawnThreshold = _threshold;
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

        CalculateSpawnPositions(planeMesh); 
       
        DrawVegetation(vegetationMesh);


        return new MeshFace(meshRenderer, meshFilter);
    }




    public void CalculateSpawnPositions(Mesh _planeMesh)
    {


        vegetationSpawnPositions.Clear();
        planePositions = _planeMesh.vertices;

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
        Debug.Log($"The Mesh for the Vegetation containts {vegetationSpawnPositions.Count} positions");
       // return vegetationSpawnPositions;
    }

    private void DrawVegetation(Mesh _mesh)
    {
       
        int vertexCount = vegetationSpawnPositions.Count * 4;
        Debug.Log($"The Mesh for the Vegetation contains {vertexCount} index. Which should be 4 times the Count of {vegetationSpawnPositions.Count} ");

        int triangleIndexCount = 3 * 2 * vertexCount; // vermutlich Error
        Debug.Log($"The Mesh for the Vegetations contains {triangleIndexCount} triangle indexes.");

        Vector3[] verts = new Vector3[vertexCount];
        int[] triangles = new int[triangleIndexCount]; // vermutlich Error


        int vertindex = 0;
        int triIndex = 0;
        for (int i = 0; i < vegetationSpawnPositions.Count; i++)
        {
            verts[vertindex] = vegetationSpawnPositions[i];
            verts[vertindex + 1] = vegetationSpawnPositions[i] + Vector3.right;
            verts[vertindex + 2] = vegetationSpawnPositions[i] + Vector3.up;
            verts[vertindex + 3] = vegetationSpawnPositions[i] + Vector3.up + Vector3.right;
            vertindex += 4;



            triangles[triIndex + 0] = (i * 4);
            triangles[triIndex + 1] = (i * 4) + 3;
            triangles[triIndex + 2] = (i * 4) + 2;

            triangles[triIndex + 3] = (i * 4);
            triangles[triIndex + 4] = (i * 4) + 1;
            triangles[triIndex + 5] = (i *4) + 3;

            triIndex += 6;
        }
        Debug.Log($"VegManager builded an vertex array of {verts.Length} vertices ");
        Debug.Log($"VegManager builded an triangle array of {triangles.Length} indexes");

        _mesh.Clear();
        _mesh.vertices = verts;
        Debug.Log($"VegManager set the veticies of {_mesh.name} to {_mesh.vertices.Length}");
        _mesh.triangles = triangles;
        Debug.Log($"VegManager set the triangleIndexCount of {_mesh.name} to {_mesh.triangles.Length}");
        _mesh.RecalculateNormals();

    }
}
