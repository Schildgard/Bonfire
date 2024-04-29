using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    public static EnemySpawnManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField] private List<EnemyData> EnemyStorage = new List<EnemyData>();



    public void RespawnList()
    {

        EnemyStateMachine[] EnemiesToDelete = FindObjectsByType<EnemyStateMachine>(FindObjectsSortMode.None);
        foreach (var enemy in EnemiesToDelete)
        {
            Destroy(enemy.gameObject);
        }

        foreach (EnemyData enemy in EnemyStorage) 
        {
            enemy.SpawnThisEnemy();
        }

    }

    [Serializable]
    public class EnemyData
    {

        public GameObject EnemyPrefab;

        public Transform EnemySpawnPosition;

        public EnemyData(GameObject _enemyPrefab, Transform _enemySpawnPosition)
        {
            EnemyPrefab = _enemyPrefab;
            EnemySpawnPosition = _enemySpawnPosition;
            
        }


        public void SpawnThisEnemy() 
        {
            Instantiate(EnemyPrefab, EnemySpawnPosition.position,Quaternion.identity);
            
        }

    }
}
