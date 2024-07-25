using System.Collections.Generic;
using UnityEngine;

public class InstancedMesh_VegetationGenerator : EnvironmentGenerator
{

    public InstancedMesh_VegetationGenerator(Mesh _planeMesh, float _threshold, Vector3 _offset, Vector3 _scaleMultiplier, bool _randomRotation, bool _randomizedOffset, float _maxYPos)
    {
        noise = new Noise();
        planeMesh = _planeMesh;

        matrices = CalculateMatrices();

        Threshold = _threshold;
        Offset = _offset;
        ScaleMultiplier = _scaleMultiplier;
        randomRotation = _randomRotation;
        randomizedOffset = _randomizedOffset;

        renderMesh = null;

        maxYPosition = _maxYPos;
    }

    public InstancedMesh_VegetationGenerator(Mesh _planeMesh, float _threshold, Vector3 _offset, Vector3 _scaleMultiplier, Mesh _mesh, bool _randomRotation, bool _randomizedOffset, float _maxYPos)
    {
        noise = new Noise();
        planeMesh = _planeMesh;

        matrices = CalculateMatrices();

        Threshold = _threshold;
        Offset = _offset;
        randomRotation = _randomRotation;
        randomizedOffset = _randomizedOffset;
        ScaleMultiplier = _scaleMultiplier;

        renderMesh = _mesh;
        maxYPosition = _maxYPos;
    }



    public List<List<Matrix4x4>> CalculateMatrices()
    {
        List<Vector3> vegetationSpawnPositions = CalculateSpawnPositions(planeMesh);
        List<List<Matrix4x4>> ListofMatrixLists = new List<List<Matrix4x4>>
        {
            new List<Matrix4x4>()
        };

        //These variables shortens the expression for adding the offset and random height to each matrix.

        int ListIndex = 0;
        int counter = 0;

        Vector3 matrixPosition;
        Vector3 randomizedHeightScale;
        for (int i = 0; i < vegetationSpawnPositions.Count; i++)
        {
            if (counter >= 1000)
            {
                ListofMatrixLists.Add(new List<Matrix4x4>());

                ListIndex++;

                counter = 0;
            }

            if (randomizedOffset)
            {
                Offset = new Vector3(Random.Range(-0.5f, 0.66f), 0f, Random.Range(-0.5f, 0.6f));
            }

            matrixPosition = new Vector3(vegetationSpawnPositions[i].x + Offset.x, vegetationSpawnPositions[i].y, vegetationSpawnPositions[i].z + Offset.z);
            randomizedHeightScale = new Vector3(ScaleMultiplier.x, Random.Range(ScaleMultiplier.y * 0.8f, ScaleMultiplier.y * 1.2f), ScaleMultiplier.z);

            if (randomRotation)
            {
                //Insert randomized Rotation here
                ListofMatrixLists[ListIndex].Add(Matrix4x4.TRS(matrixPosition, Quaternion.Euler(0, Random.Range(0, 181), 0), randomizedHeightScale));
            }
            else
            {
                // ListofMatrixLists[ListIndex].Add(Matrix4x4.TRS(vegetationSpawnPositions[i] + Offset, Quaternion.identity, ScaleMultiplier));
                ListofMatrixLists[ListIndex].Add(Matrix4x4.TRS(matrixPosition, Quaternion.identity, randomizedHeightScale));
            }
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

        if (renderMesh == null)
        {
            Mesh mesh = new Mesh();

            Vector3[] verts = new Vector3[4];
            int[] triangles = new int[6];
            Vector2[] uvs = new Vector2[verts.Length];

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

            for (int i = 0; i < verts.Length; i++)
            {
                uvs[i] = new Vector2(verts[i].x, verts[i].y);
            }

            mesh.Clear();
            mesh.vertices = verts;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();


            return mesh;
        }
        else
        {
            return renderMesh;
        }
    }

}
