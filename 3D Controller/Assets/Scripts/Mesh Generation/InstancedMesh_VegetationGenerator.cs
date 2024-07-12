using System.Collections.Generic;
using UnityEngine;

public class InstancedMesh_VegetationGenerator : EnvironmentGenerator
{
    public InstancedMesh_VegetationGenerator(Mesh _planeMesh, EnvironmentalSettings _environmentalSettings)
    {
        noise = new Noise();
        environmentalSettings = _environmentalSettings;
        planeMesh = _planeMesh;

        matrices = CalculateMatrices();

    }



    public List<List<Matrix4x4>> CalculateMatrices()
    {
        List<Vector3> vegetationSpawnPositions = CalculateSpawnPositions(planeMesh);


        List<List<Matrix4x4>> ListofMatrixLists = new List<List<Matrix4x4>>
        {
            new List<Matrix4x4>()
        };

        int ListIndex = 0;
        int counter = 0;

        for (int i = 0; i < vegetationSpawnPositions.Count; i++)
        {
            if (counter >= 1000)
            {
                ListofMatrixLists.Add(new List<Matrix4x4>());

                ListIndex++;

                counter = 0;
            }

            ListofMatrixLists[ListIndex].Add(Matrix4x4.TRS(vegetationSpawnPositions[i] + EnvironmentalSettings.Offset, Quaternion.identity, EnvironmentalSettings.ScaleMultiplier));
            counter++;

        }
        return ListofMatrixLists;
    }



    public override Mesh CreateEnvironmentalMesh()
    {
        matrices = CalculateMatrices();

        Mesh instancablMesh = GenerateMesh();
        return instancablMesh;
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
