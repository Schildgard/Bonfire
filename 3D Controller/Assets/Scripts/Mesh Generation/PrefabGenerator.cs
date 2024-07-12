using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGenerator
{
    private Mesh planeMesh;
    private Noise noise;

    protected EnvironmentalSettings environmentalSettings;
    public EnvironmentalSettings EnvironmentalSettings => environmentalSettings;

    private Vector3[] planePositions;
    public Vector3[] PlanePositions => planePositions;

    public List<Vector3> SpawnPositions = new List<Vector3>();

    public PrefabGenerator(Mesh _mesh, EnvironmentalSettings _environmentalSettings)
    {
        planeMesh = _mesh;
        noise = new Noise();
        environmentalSettings = _environmentalSettings;
    }


    public virtual List<Vector3> CalculateSpawnPositions(Mesh _planeMesh)
    {
        SpawnPositions.Clear();
        planePositions = _planeMesh.vertices;

        float spawnValue;
        //Evaluate Positions in Noise so it returns a Value between 0 and 1

        foreach (var position in planePositions)
        {
            spawnValue = noise.Evaluate(position);

            if (spawnValue > environmentalSettings.Threshold)
            {
                SpawnPositions.Add(position);
            }
        }

        return SpawnPositions;
    }


    public void SetSpawnPositions()
    {
      SpawnPositions =  CalculateSpawnPositions(planeMesh);
    }
}
