using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class TaskMaster : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private EnemyStateMachine[] enemyArray;
    private TransformAccessArray accessArray;
    private Transform[] enemyTransforms;


    void Start()
    {
        Initialize();
    }


    private void Update()
    {
        NativeArray<float> result = new NativeArray<float>(accessArray.length, Allocator.TempJob);

        Job_EnemyDetection detectionJob = new Job_EnemyDetection
        {
            playerPosition = playerTransform.position,
            results = result,
        };

        JobHandle jobHandle = detectionJob.Schedule(accessArray);
        jobHandle.Complete();


        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (detectionJob.results[i] >= 0.7f)
            {
                enemyArray[i].TaskBool = true;
            }
            else enemyArray[i].TaskBool = false;
        }
        result.Dispose();

    }





    private void Initialize()
    {
        enemyArray = FindObjectsByType<EnemyStateMachine>(sortMode: FindObjectsSortMode.None);
        enemyTransforms = new Transform[enemyArray.Length];

        for (int i = 0; i < enemyArray.Length; i++)
        {
            enemyTransforms[i] = enemyArray[i].transform;
        }

        accessArray = new TransformAccessArray(enemyTransforms);

    }

    private void OnDisable()
    {
        accessArray.Dispose();
    }

}
