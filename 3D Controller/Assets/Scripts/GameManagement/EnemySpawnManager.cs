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


    [SerializeField] public List<EnemyID> EnemyList = new List<EnemyID>();
    EnemyID[] EnemysInScene;



    [SerializeField] private List<GameObject> KnownEnemies;

    private void Start()
    {
        InitializeList();
    }

    public void InitializeList()
    {
         EnemysInScene = FindObjectsOfType(typeof(EnemyID)) as EnemyID[];

        foreach (EnemyID enemy in EnemysInScene)
        {
            EnemyList.Add(enemy);
            enemy.ResetEnemy();

            
        }

        Debug.Log($"EnemyList containts {EnemyList.Count} Items");
    }




    public void RespawnList()
    {

        foreach (var enemy in EnemyList) 
        {
            enemy.transform.position = enemy.SpawnPosition;
        }
    }
}
