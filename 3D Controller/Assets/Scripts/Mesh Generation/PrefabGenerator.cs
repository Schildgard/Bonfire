using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabGenerator
{
    private Mesh planeMesh;
    private Noise noise;
    private int maxSpawnCount;
    private bool enableMaxCount = true;
    private Vector3[] planePositions;
    private float Threshold = -1f;
    private Vector3 Offset = new Vector3(0,0,0);
    private bool randomizedOffset = false;



    public Vector3[] PlanePositions => planePositions;

    public List<Vector3> SpawnPositions = new List<Vector3>();


    public PrefabGenerator(Mesh _mesh, int _maxSpawnCount, bool _enableMaxCount, float _threshold, Vector3 _offset, bool _randomOffset)
    {
        planeMesh = _mesh;
        noise = new Noise();
        maxSpawnCount = _maxSpawnCount;
        enableMaxCount = _enableMaxCount;

        Threshold = _threshold;
        Offset = _offset;
        randomizedOffset = _randomOffset;
    }


    public virtual List<Vector3> CalculateSpawnPositions(Mesh _planeMesh)
    {
        SpawnPositions.Clear();
        planePositions = _planeMesh.vertices;
        Vector3 offsetPosition;
        float spawnValue;
        if (enableMaxCount)
        {
            for (int i = 0, c = 0; i < planePositions.Length; i++, c++)
            {
                if (c >= maxSpawnCount) return SpawnPositions;

                Vector3 randomizedPosition = planePositions[Random.Range(0, planePositions.Length)];
                randomizedPosition.y = randomizedPosition.y + Offset.y;
                SpawnPositions.Add(randomizedPosition);
            }
            return SpawnPositions;
        }

        else
        {
            foreach (var position in planePositions)
            {
                //Evaluate Positions in Noise so it returns a Value between 0 and 1
                spawnValue = noise.Evaluate(position);

                if (spawnValue > Threshold)
                {
                    if (randomizedOffset)
                    {
                        Offset = new Vector3(Random.Range(-0.5f, 0.6f), 0, Random.Range(-0.5f, 0.6f));
                    }
                    offsetPosition = new Vector3(position.x + Offset.x, position.y + Offset.y, position.z + Offset.z);
                    SpawnPositions.Add(offsetPosition);
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
