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



    [SerializeField] private List<EnemyData> EnemyStorage;

    private List<EnemyData> EnemyRespawnList = new List<EnemyData>();

    //private List<Transform> SpawnPosition = new List<Transform>();

    [SerializeField] private EnemyData SoulsCrate;

    [SerializeField] private EnemyData Player;



    private void Start()
    {
        // foreach (EnemyData Enemy in EnemyStorage)
        // {
        //     SpawnPosition.Add(Enemy.EnemySpawnPosition);
        // }

        EnemyRespawnList = EnemyStorage;
    }


    public void RespawnList()
    {

       EnemyStateMachine[] EnemiesToDelete = FindObjectsByType<EnemyStateMachine>(FindObjectsSortMode.None);
       foreach (var enemy in EnemiesToDelete)
       {
           Destroy(enemy.gameObject);
       }

 

      // foreach (EnemyData enemy in EnemyStorage) 
      // {
      //     enemy.SpawnThisEnemy();
      // }

        for (int i = 0; i < EnemyRespawnList.Count; i++)
        {
            EnemyRespawnList[i].SpawnThisEnemy(EnemyRespawnList[i].EnemySpawnPosition.position);
        }

    }

    public void SpawnSoulsCrate() 
    {
        SoulsCrate.SpawnThisEnemy(Vector3.zero);
    }

    public void RespawnPlayer() 
    {
        Player.EnemyPrefab.transform.position = Player.EnemySpawnPosition.position;
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


        public void SpawnThisEnemy(Vector3 _position) 
        {
            Instantiate(EnemyPrefab, _position,EnemyPrefab.transform.rotation);
            
        }

        public void SpawnPlayer()
        { 

        }

    }
}
