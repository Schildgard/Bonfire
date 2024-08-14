using System.Collections.Generic;
using UnityEngine;


public abstract class EnvironmentGenerator
{

    //The whole abstract class is more like a Legacy thing from when they were two renderModes, and I used two different Generator. 
    //The discarded Generator can still be found in Discard Folder of 'Environmental Manager/Generators/Discard'.
    //I have not removed the abstract class and Inheritation of Instanced Mesh Generator because I discarded the other Generator just today, and I have more
    //important things to refactor for now.
    protected Transform planeTransform;

    protected Mesh planeMesh;
    protected Noise noise;


    protected Vector3[] planePositions;
    protected Vector3[] positionNormals;
    protected List<Vector3> vegetationNormals = new List<Vector3>();

    protected List<List<Matrix4x4>> matrices;

    public List<List<Matrix4x4>> Matrices => matrices;


    protected float threshold;
    protected float maxYPosition;
    protected Vector3 Offset;
    protected Vector3 scaleMultiplier;
    protected Mesh renderMesh;
    protected bool randomRotation;
    protected bool randomizedOffset;

    protected float minimumVertexFlatness;


    public virtual Mesh CreateEnvironmentalMesh()
    {
        return null;
    }

    public virtual List<Vector3> CalculateSpawnPositions(Mesh _planeMesh)
    {
        List<Vector3> vegetationSpawnPositions = new List<Vector3>();

        planePositions = TranslateVertexToWorldPos(_planeMesh.vertices, planeTransform);
        positionNormals = _planeMesh.normals;


        float spawnValue;

        for (int i = 0; i < planePositions.Length; i++)
        {
            spawnValue = noise.Evaluate(planePositions[i]);

            if (spawnValue >= threshold && planePositions[i].y <= maxYPosition && CompareNormalToGlobalUp(positionNormals[i]) >= minimumVertexFlatness) // Add Normal Comparison
            {
                vegetationSpawnPositions.Add(planePositions[i]);
                vegetationNormals.Add(positionNormals[i]);
            }
        }

        return vegetationSpawnPositions;

    }

    private float CompareNormalToGlobalUp(Vector3 _normal)
    {
        float dotProduct = Vector3.Dot(Vector3.up, _normal);
        return dotProduct;
    }

    private Vector3[] TranslateVertexToWorldPos(Vector3[] _inputArray, Transform _transform)
    {
        Vector3[] worldPositions = new Vector3[_inputArray.Length];

        for (int i = 0; i < _inputArray.Length; i++)
        {
            worldPositions[i] = _transform.TransformPoint(_inputArray[i]);
        }

        return worldPositions;
    }
}
