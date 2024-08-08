using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnvironmentGenerator
{
    protected Mesh planeMesh;
    protected Noise noise;


    protected Vector3[] planePositions;
    protected Vector3[] positionNormals;
    protected List<Vector3> vegetationNormals = new List<Vector3>();

    protected List<List<Matrix4x4>> matrices;

    public List<List<Matrix4x4>> Matrices => matrices;


    protected float Threshold;
    protected float maxYPosition;

    protected Vector3 Offset;
    protected Vector3 ScaleMultiplier;

    protected Mesh renderMesh;

    protected bool randomRotation;
    protected bool randomizedOffset;



    public virtual Mesh CreateEnvironmentalMesh()
    {
        Debug.Log($"This Generator ({this}) creates no Mesh. It probably is a a Prefab Generator which calculates Plane Positions to spawn its Prefabs");
        return null;
    }

    public virtual List<Vector3> CalculateSpawnPositions(Mesh _planeMesh)
    {
        List<Vector3> vegetationSpawnPositions = new List<Vector3>();

        vegetationSpawnPositions.Clear();
        planePositions = _planeMesh.vertices;
        positionNormals = _planeMesh.normals;


        float spawnValue;

        for (int i = 0; i < planePositions.Length; i++)
        {
            spawnValue = noise.Evaluate(planePositions[i]);

            if (spawnValue >= Threshold && planePositions[i].y <= maxYPosition && CompareNormalToGlobalUp(positionNormals[i]) >= 0.95f) // Add Normal Comparison
            {
                vegetationSpawnPositions.Add(planePositions[i]);
                vegetationNormals.Add(positionNormals[i]);
               // Debug.Log($"")
            }
        }

        return vegetationSpawnPositions;

    }

    private float CompareNormalToGlobalUp(Vector3 _normal)
    {
        float dotProduct = Vector3.Dot(Vector3.up, _normal);
        return dotProduct;
    }
}
