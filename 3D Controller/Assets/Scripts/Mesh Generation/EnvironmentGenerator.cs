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
        //Evaluate Positions in Noise so it returns a Value between 0 and 1

      //  foreach (var position in planePositions)
      //  {
      //      spawnValue = noise.Evaluate(position);
      //
      //      if (spawnValue >= Threshold && position.y <= maxYPosition)
      //      {
      //          vegetationSpawnPositions.Add(position);
      //      }
      //  }



        for (int i = 0; i < planePositions.Length; i++)
        {
            spawnValue = noise.Evaluate(planePositions[i]);

            if (spawnValue >= Threshold && planePositions[i].y <= maxYPosition)
            {
                vegetationSpawnPositions.Add(planePositions[i]);
                vegetationNormals.Add(positionNormals[i]);
            }
        }

        return vegetationSpawnPositions;

    }
}
