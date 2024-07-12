using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGenerator
{
    protected Mesh planeMesh;
    protected Noise noise;
    protected GameObject prefab;

    protected EnvironmentalSettings environmentalSettings;
    public EnvironmentalSettings EnvironmentalSettings => environmentalSettings;

    private Vector3[] planePositions;
    public Vector3[] PlanePositions => planePositions;

    public PrefabGenerator(Mesh _mesh, EnvironmentalSettings _environmentalSettings, GameObject _prefab)
    {
        planeMesh = _mesh;
        noise = new Noise();
        environmentalSettings = _environmentalSettings;
        prefab = _prefab;
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

            if (spawnValue > environmentalSettings.Threshold)
            {
                vegetationSpawnPositions.Add(position);
            }
        }
        return vegetationSpawnPositions;
    }


    public void SetSpawnPositions()
    {
        CalculateSpawnPositions(planeMesh);
    }
}
