using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabGenerator
{
    private Mesh planeMesh;
    private Noise noise;
    private int maxSpawnCount;
    private bool enableMaxCount;

    protected EnvironmentalSettings environmentalSettings;
    public EnvironmentalSettings EnvironmentalSettings => environmentalSettings;

    private Vector3[] planePositions;
    public Vector3[] PlanePositions => planePositions;

    public List<Vector3> SpawnPositions = new List<Vector3>();

    public PrefabGenerator(Mesh _mesh, EnvironmentalSettings _environmentalSettings, int _maxSpawnCount, bool _enableMaxCount)
    {
        planeMesh = _mesh;
        noise = new Noise();
        environmentalSettings = _environmentalSettings;
        maxSpawnCount = _maxSpawnCount;
        enableMaxCount = _enableMaxCount;
    }


    public virtual List<Vector3> CalculateSpawnPositions(Mesh _planeMesh)
    {
        SpawnPositions.Clear();
        planePositions = _planeMesh.vertices;

        float spawnValue;
        //Evaluate Positions in Noise so it returns a Value between 0 and 1
        if (enableMaxCount)
        {
            for (int i = 0, c = 0; i < planePositions.Length; i++, c++)
            {
                if (c >= maxSpawnCount) return SpawnPositions;
                Vector3 randomizedPosition = planePositions[Random.Range(0, planePositions.Length)];
                SpawnPositions.Add(randomizedPosition);
            }
            return SpawnPositions;
        }
        else
        {
            foreach (var position in planePositions)
            {
                spawnValue = noise.Evaluate(position);

                if (spawnValue > environmentalSettings.Threshold)
                {
                    SpawnPositions.Add(position);
                }
            }
        }
        return SpawnPositions;
    }


    public void SetSpawnPositions()
    {
        SpawnPositions = CalculateSpawnPositions(planeMesh);
    }
}
