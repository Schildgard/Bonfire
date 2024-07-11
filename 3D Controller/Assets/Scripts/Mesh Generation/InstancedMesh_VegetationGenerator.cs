using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstancedMesh_VegetationGenerator
{


    public InstancedMesh_VegetationGenerator(Mesh _mesh, Material _material, float _threshold)
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



    public Mesh[] GenerateVegetationItem()
    {

        List<Vector3> SpawnPositions = CalculateSpawnPositions(planeMesh);

        Mesh[] Meshes = DrawVegetation(SpawnPositions);


        return Meshes;
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

    private Mesh[] DrawVegetation(List<Vector3> _spawnPosition)
    {
        Mesh[] newMeshArray = new Mesh[_spawnPosition.Count];

        int i = 0;

        foreach (var position in _spawnPosition)
        {

            GameObject VegetationObject = new GameObject();

            MeshFilter meshFilter = VegetationObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = VegetationObject.AddComponent<MeshRenderer>();
            Mesh vegetationMesh = new Mesh();

            vegetationMesh.name = "Vegetation";
            meshRenderer.sharedMaterial = material;
            meshFilter.mesh = vegetationMesh;

            Vector3[] verts = new Vector3[4];
            int[] triangles = new int[6];

            verts[0] = position;
            verts[1] = position + Vector3.right;
            verts[2] = position + Vector3.up;
            verts[3] = position + Vector3.up + Vector3.right;

            triangles[0] = 0;
            triangles[1] = 3;
            triangles[2] = 2;

            triangles[3] = 0;
            triangles[4] = 1;
            triangles[5] = 3;



            vegetationMesh.Clear();
            vegetationMesh.vertices = verts;
            vegetationMesh.triangles = triangles;
            vegetationMesh.RecalculateNormals();
            newMeshArray[i] = vegetationMesh;
            i++;
        }
        return newMeshArray;
    }

    public Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] verts = new Vector3[4];
        int[] triangles = new int[6];

        verts[0] = Vector3.zero;
        verts[1] = Vector3.right;
        verts[2] = Vector3.up;
        verts[3] = Vector3.up + Vector3.right;

        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 1;
        triangles[5] = 3;

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
