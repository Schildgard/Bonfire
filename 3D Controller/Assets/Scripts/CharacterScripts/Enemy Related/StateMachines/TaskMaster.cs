using System.Threading.Tasks;
using UnityEngine;

public class TaskMaster : MonoBehaviour
{
    private Task runTask;
    private EnemyStateMachine[] EnemyList;
    private bool exitTask;

    void Start()
    {
        EnemyList = FindObjectsByType<EnemyStateMachine>(sortMode:FindObjectsSortMode.None);

        runTask = Task.Run(ContinousTask);


    }

    private void Update()
    {
        
    }

    public void ContinousTask()
    {
        while (!exitTask)
        {

            foreach (var Enemy in EnemyList)
            {
                Enemy.taskBool = MT_CheckEnemyVision(Enemy.transform, Enemy.PlayerPosition, Enemy);
            }
        }
    }


    public bool MT_CheckEnemyVision(Transform _currentPosition, Transform _targetPosition, EnemyStateMachine _enemy)
    {
        float dotproduct = GetRadius(_currentPosition, _targetPosition,_enemy);
        return dotproduct >= 0.7f ? true : false;
    }
    protected float GetRadius(Transform _currentPosition, Transform _targetPosition, EnemyStateMachine _enemy)
    {
        Vector3 ViewDirection = _currentPosition.forward;

        Vector3 TargetDirection = (_targetPosition.position - _currentPosition.position);


        float magnitudeViewDirection = Vector3.Magnitude(ViewDirection);
        float magnitudeDistanceDirection = Vector3.Magnitude(TargetDirection);

        float dotProduct = (ViewDirection.x * TargetDirection.x) + (ViewDirection.y * TargetDirection.y) + (ViewDirection.z * TargetDirection.z);

        float degrees = dotProduct / (magnitudeViewDirection * magnitudeDistanceDirection);

        if (TargetDirection.magnitude <= _enemy.EnemyDetection.ViewRange)
        {
            return degrees;
        }
        else return 0f;



    }
}
