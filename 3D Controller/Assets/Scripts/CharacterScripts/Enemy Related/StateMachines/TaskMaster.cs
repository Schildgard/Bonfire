using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Rendering;

public class TaskMaster : MonoBehaviour
{
    private EnemyStateMachine[] EnemyArray;

    private TransformAccessArray AccessArray;
    private Transform[] EnemyTransforms;


    [SerializeField] private Transform playerTransform;




    void Start()
    {
        Initialize();
    }


    private void Update()
    {

        Debug.Log("TaskMaster tries to create an Job Object");
        Job_EnemyDetection detectionJob = new Job_EnemyDetection
        {
            playerPosition = playerTransform.position,
            //results = playerAnglesToEnemy,
        };
        Debug.Log("TaskMaster sucessfully created Job Object");


        Debug.Log("TaskMaster creates Job Handle for its Job");
        JobHandle jobHandle = detectionJob.Schedule(AccessArray);

        jobHandle.Complete();


        for (int i = 0; i < EnemyArray.Length; i++)
        {

            if (detectionJob.results >= 0.7f)
            {
                Debug.Log($"{EnemyArray[i].name} sees the Player: dotproduct is {detectionJob.results}");
            }
            else
                Debug.Log($"{EnemyArray[i].name} does not see the Player:  dotproduct is {detectionJob.results}");
        }
        
    }





    private void Initialize()
    {
        EnemyArray = FindObjectsByType<EnemyStateMachine>(sortMode: FindObjectsSortMode.None);
        Debug.Log($"TaskMaster added {EnemyArray.Length} StateMachines to its StateMachineArray");

        EnemyTransforms = new Transform[EnemyArray.Length];
        Debug.Log($"TaskMaster initialized the EnemyTransform Array with the size of {EnemyTransforms.Length}");


        for (int i = 0; i < EnemyArray.Length; i++)
        {
            EnemyTransforms[i] = EnemyArray[i].transform;
        }
        Debug.Log($"TaskMaster filled the EnemyTransformArray with {EnemyTransforms.Length} enemies. ");

        AccessArray = new TransformAccessArray(EnemyTransforms);

        Debug.Log("TaskMaster completed its Initialization");

    }

    private void OnDisable()
    {
        AccessArray.Dispose();
    }

}
