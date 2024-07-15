using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationGenerator : EnvironmentGenerator
{
    private Material material;


    public VegetationGenerator(Mesh _mesh, Material _material, float _threshold, Vector3 _offset, Vector3 _scaleMultiplier)
    {
        planeMesh = _mesh;
        material = _material;
        noise = new Noise();

        Threshold = _threshold;
        Offset = _offset;
        ScaleMultiplier = _scaleMultiplier;
    }



    public Mesh GenerateVegetationItem()
    {

        GameObject VegetationPlaneObject = new GameObject();

        MeshFilter meshFilter = VegetationPlaneObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = VegetationPlaneObject.AddComponent<MeshRenderer>();
        Mesh vegetationMesh = new Mesh();

        vegetationMesh.name = "Vegetation";
        meshRenderer.sharedMaterial = material;
        meshFilter.mesh = vegetationMesh;

        List<Vector3> SpawnPositions = CalculateSpawnPositions(planeMesh);

        DrawVegetationMesh(vegetationMesh, SpawnPositions);

        return vegetationMesh;
    }

    private void DrawVegetationMesh(Mesh _mesh, List<Vector3> _spawnPosition)
    {
        int vertexCount = _spawnPosition.Count * 4;

        int triangleIndexCount = 3 * 2 * vertexCount;

        Vector3[] verts = new Vector3[vertexCount];
        int[] triangles = new int[triangleIndexCount];

        Vector2[] uvs = new Vector2[vertexCount];


        int vertindex = 0;
        int triIndex = 0;
        Vector3 rightMovement = Vector3.right * ScaleMultiplier.x;
        Vector3 upMovement = Vector3.up * ScaleMultiplier.y;
        // Vector3 Offset = environmentalSettings.Offset;

        for (int i = 0; i < _spawnPosition.Count; i++)
        {
            _spawnPosition[i] += Offset;
            verts[vertindex] = _spawnPosition[i];
            verts[vertindex + 1] = _spawnPosition[i] + rightMovement;
            verts[vertindex + 2] = _spawnPosition[i] + upMovement;
            verts[vertindex + 3] = _spawnPosition[i] + upMovement + rightMovement;
            vertindex += 4;




            triangles[triIndex + 0] = (i * 4);
            triangles[triIndex + 1] = (i * 4) + 3;
            triangles[triIndex + 2] = (i * 4) + 2;

            triangles[triIndex + 3] = (i * 4);
            triangles[triIndex + 4] = (i * 4) + 1;
            triangles[triIndex + 5] = (i * 4) + 3;

            triIndex += 6;
        }

        for (int i = 0; i < verts.Length; i++)
        {
            uvs[i] = new Vector2(verts[i].x, verts[i].y);
        }

        _mesh.Clear();
        _mesh.vertices = verts;
        _mesh.triangles = triangles;
        _mesh.uv = uvs;
        _mesh.RecalculateNormals();
    }

    public override Mesh CreateEnvironmentalMesh()
    {
        Mesh newMesh = GenerateVegetationItem();
        return newMesh;
    }
}
