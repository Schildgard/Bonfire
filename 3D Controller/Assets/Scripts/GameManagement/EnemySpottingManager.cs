using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpottingManager : MonoBehaviour
{
    [SerializeField] private List<EnemyDetectionScript> EnemiesInScene;

    private void Update()
    {
        foreach (var enemy in EnemiesInScene)
        {
            if (enemy.Detected)
            {
                enemy.Alarm();
            }
        }
    }

}




public class Data
{
    public EnemyScript enemyScript;
    public EnemyDetectionScript detectionScript;


}