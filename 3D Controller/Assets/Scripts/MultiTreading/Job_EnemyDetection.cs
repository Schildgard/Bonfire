using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Burst;

[BurstCompile]
public struct Job_EnemyDetection : IJobParallelForTransform
{
    [ReadOnly] public float3 playerPosition;

    public NativeArray<float> results;

    public void Execute(int _enemyListCount, TransformAccess _transform)
    {

        Vector3 forwardDirection = _transform.rotation * Vector3.forward;
        float3 viewDirection = new float3(forwardDirection.x, forwardDirection.y, forwardDirection.z);


        float3 directionToTarget = (playerPosition - (float3)_transform.position);

        float magnitudeOfViewDirection = math.length(viewDirection);
        float magnitudeOfDistanceDirection = math.length(directionToTarget);

        float dotProduct = (viewDirection.x * directionToTarget.x) + (viewDirection.y * directionToTarget.y) + (viewDirection.z * directionToTarget.z);
        float degrees = dotProduct / (magnitudeOfViewDirection * magnitudeOfDistanceDirection);

        results[_enemyListCount] = degrees;


        for (int i = 0; i < 100000; i++)
        {
            float testfloat = Mathf.Sqrt(i) / 20f;
        }

        
    }
}
