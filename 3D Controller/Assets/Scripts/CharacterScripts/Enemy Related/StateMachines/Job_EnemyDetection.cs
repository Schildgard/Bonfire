using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;

public struct Job_EnemyDetection : IJobParallelForTransform
{
    [ReadOnly] public float3 playerPosition;

    [ReadOnly] public float results;


    public void Execute(int _enemyListCount, TransformAccess _transform)
    {
        //Need to get local forward coordinate of _transform and replace it in the calculations with _transform.position


        float3 forwardDirection = new float3(_transform.rotation.x,_transform.rotation.y,_transform.rotation.z);


        //float3 targetDirection = (playerPosition - (float3)_transform.position);
        float3 targetDirection = (playerPosition - forwardDirection);


        //float magnitudeOfViewDirection = math.length(_transform.position);
        float magnitudeOfViewDirection = math.length(forwardDirection);
        float magnitudeOfDistanceDirection = math.length(targetDirection);



        //float dotProduct = (_transform.position.x * targetDirection.x) + (_transform.position.y * targetDirection.y) + (_transform.position.z * targetDirection.z);
        float dotProduct = (forwardDirection.x * targetDirection.x) + (forwardDirection.y * targetDirection.y) + (forwardDirection.z * targetDirection.z);

        float degrees = dotProduct / (magnitudeOfViewDirection * magnitudeOfDistanceDirection);

        results = degrees;
    }
}
