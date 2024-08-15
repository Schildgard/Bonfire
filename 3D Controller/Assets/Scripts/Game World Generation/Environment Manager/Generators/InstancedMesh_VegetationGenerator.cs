using System.Collections.Generic;
using UnityEngine;

public class InstancedMesh_VegetationGenerator : EnvironmentGenerator
{

    public InstancedMesh_VegetationGenerator(Mesh _planeMesh, float _threshold, Vector3 _scaleMultiplier, bool _randomRotation, bool _randomizedOffset, float _maxYPos, Transform _planeTransform, float _minimumVertexFlatness)
    {
        // This Constructor operates when no Mesh is transmitted.  The Material will be rendered on a Quad Mesh in that Case.
        //It can probably be ignored in 99% of cases and will be removed later.
        noise = new Noise();
        planeMesh = _planeMesh;

        threshold = _threshold;

        scaleMultiplier = _scaleMultiplier;
        randomRotation = _randomRotation;
        randomizedOffset = _randomizedOffset;

        renderMesh = null;
        maxYPosition = _maxYPos;

        planeTransform = _planeTransform;
        minimumVertexFlatness = _minimumVertexFlatness;
    }

    public InstancedMesh_VegetationGenerator(Mesh _planeMesh, float _threshold, Vector3 _scaleMultiplier, Mesh _mesh, bool _randomRotation, bool _randomizedOffset, float _maxYPos, Transform _planeTransform, float _minimumVertexFlatness)
    {
        noise = new Noise();
        planeMesh = _planeMesh;

        threshold = _threshold;

        scaleMultiplier = _scaleMultiplier;
        randomRotation = _randomRotation;
        randomizedOffset = _randomizedOffset;

        renderMesh = _mesh;
        maxYPosition = _maxYPos;

        planeTransform = _planeTransform;
        minimumVertexFlatness = _minimumVertexFlatness;
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

        //These variables shortens the expression for adding the offset and random height to each matrix.
        Vector3 matrixPosition;
        Quaternion matrixNormal;
        Vector3 randomizedHeightScale;

        for (int i = 0; i < vegetationSpawnPositions.Count; i++)
        {
            // GPU Instancing can calculte about 1100~ instances per Matrix List, so every 1000 units, a new List has to be added.
            if (counter >= 1000)
            {
                ListofMatrixLists.Add(new List<Matrix4x4>());
                ListIndex++;
                counter = 0;
            }

            #region Calculation of SpawnPosition and Rotation
            if (randomizedOffset)
            {
                Offset = new Vector3(Random.Range(-0.5f, 0.66f), 0f, Random.Range(-0.5f, 0.6f));
            }

            matrixPosition = new Vector3(vegetationSpawnPositions[i].x + Offset.x, vegetationSpawnPositions[i].y - 0.2f, vegetationSpawnPositions[i].z + Offset.z);
            matrixNormal = Quaternion.LookRotation(Vector3.forward, vegetationNormals[i]);

            randomizedHeightScale = new Vector3(scaleMultiplier.x, Random.Range(scaleMultiplier.y * 0.8f, scaleMultiplier.y * 1.2f), scaleMultiplier.z);
            #endregion

            if (randomRotation)
            {
                ListofMatrixLists[ListIndex].Add(Matrix4x4.TRS(matrixPosition, Quaternion.Euler(matrixNormal.x, Random.Range(0, 181), matrixNormal.z), randomizedHeightScale));
            }
            else
            {
                ListofMatrixLists[ListIndex].Add(Matrix4x4.TRS(matrixPosition, matrixNormal, randomizedHeightScale));
            }
            counter++;

        }
        return ListofMatrixLists;
    }



    public override Mesh CreateEnvironmentalMesh()
    {
        matrices = CalculateMatrices();

        Mesh instanceableMesh = GenerateMesh();
        return instanceableMesh;
    }

    public Mesh GenerateMesh()
    {
        // If Constructor was calles without an transmitted Mesh, render a Quad Shape. Probably about to get removed very soon.
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
