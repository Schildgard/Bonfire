using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabGenerator
{
    private Transform planeTransform;
    private Mesh planeMesh;
    private Noise noise;
    private int maxSpawnCount;
    private bool enableMaxCount = true;
    private Vector3[] planePositions;
    private float Threshold = -1f;
    private Vector3 Offset = new Vector3(0, 0, 0);
    private bool randomizedOffset = false;

    private float maxYPosition;

    public List<Vector3> SpawnPositions = new List<Vector3>();


    public PrefabGenerator(Mesh _mesh, int _maxSpawnCount, bool _enableMaxCount, float _threshold, Vector3 _offset, bool _randomOffset, Transform _planeTransform, float _maxYPosition)
    {
        planeMesh = _mesh;
        planeTransform = _planeTransform;
        noise = new Noise();
        maxSpawnCount = _maxSpawnCount;
        enableMaxCount = _enableMaxCount;

        Threshold = _threshold;
        Offset = _offset;
        randomizedOffset = _randomOffset;

        maxYPosition = _maxYPosition;
    }


    public virtual List<Vector3> CalculateSpawnPositions(Mesh _planeMesh)
    {
        SpawnPositions.Clear();
        planePositions = TranslateVertexToWorldPos(_planeMesh.vertices, planeTransform);
        Vector3 offsetPosition;
        float spawnValue;


        if (enableMaxCount)
        {
            List<Vector3> filteredPositions = FilterPositionsForHeight(planePositions, maxYPosition);
            for (int i = 0, c = 0; i < filteredPositions.Count; i++, c++)
            {
                if (c >= maxSpawnCount) return SpawnPositions;

                 Vector3 randomizedPosition = planePositions[Random.Range(0, filteredPositions.Count)];


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

                if (position.y <= maxYPosition && spawnValue > Threshold)
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


    private Vector3[] TranslateVertexToWorldPos(Vector3[] _inputArray, Transform _transform)
    {
        Vector3[] worldPositions = new Vector3[_inputArray.Length];

        for (int i = 0; i < _inputArray.Length; i++)
        {
            worldPositions[i] = _transform.TransformPoint(_inputArray[i]);
        }

        return worldPositions;
    }

    private List<Vector3> FilterPositionsForHeight(Vector3[] _positions, float _maxHeight)
    {
        List<Vector3> validPositions = new List<Vector3>();

        foreach (var positon in _positions)
        {
            if (positon.y <= _maxHeight)
            {
                validPositions.Add(positon);
            }
        }
        return validPositions;
    }
}
