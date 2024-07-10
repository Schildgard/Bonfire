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

        List<Vector3> SpawnPositions = CalculateSpawnPositions(planeMesh); 
       
        DrawVegetation(vegetationMesh, SpawnPositions);


        return new MeshFace(meshRenderer, meshFilter);
    }




    public List<Vector3> CalculateSpawnPositions(Mesh _planeMesh)
    {
        List<Vector3> vegetationSpawnPositions = new List<Vector3>();

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
        return vegetationSpawnPositions;
    }

    private void DrawVegetation(Mesh _mesh, List<Vector3> _spawnPosition)
    {
       
        int vertexCount = _spawnPosition.Count * 4;

        int triangleIndexCount = 3 * 2 * vertexCount;

        Vector3[] verts = new Vector3[vertexCount];
        int[] triangles = new int[triangleIndexCount];


        int vertindex = 0;
        int triIndex = 0;
        for (int i = 0; i < _spawnPosition.Count; i++)
        {
            verts[vertindex] = _spawnPosition[i];
            verts[vertindex + 1] = _spawnPosition[i] + Vector3.right;
            verts[vertindex + 2] = _spawnPosition[i] + Vector3.up;
            verts[vertindex + 3] = _spawnPosition[i] + Vector3.up + Vector3.right;
            vertindex += 4;



            triangles[triIndex + 0] = (i * 4);
            triangles[triIndex + 1] = (i * 4) + 3;
            triangles[triIndex + 2] = (i * 4) + 2;

            triangles[triIndex + 3] = (i * 4);
            triangles[triIndex + 4] = (i * 4) + 1;
            triangles[triIndex + 5] = (i *4) + 3;

            triIndex += 6;
        }


        _mesh.Clear();
        _mesh.vertices = verts;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();

    }
}
