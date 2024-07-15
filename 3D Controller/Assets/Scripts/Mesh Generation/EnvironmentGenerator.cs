using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnvironmentGenerator
{
    protected Mesh planeMesh;
    protected Noise noise;


    protected Vector3[] planePositions;
    protected List<List<Matrix4x4>> matrices;

    protected EnvironmentalSettings environmentalSettings;
    public EnvironmentalSettings EnvironmentalSettings => environmentalSettings;
    public List<List<Matrix4x4>> Matrices => matrices;


    protected float Threshold;

    protected Vector3 Offset;
    protected Vector3 ScaleMultiplier;




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

        float spawnValue;
        //Evaluate Positions in Noise so it returns a Value between 0 and 1

        foreach (var position in planePositions)
        {
            spawnValue = noise.Evaluate(position);

            if (spawnValue > Threshold)
            {
                vegetationSpawnPositions.Add(position);
            }
        }
        return vegetationSpawnPositions;
    }
}
