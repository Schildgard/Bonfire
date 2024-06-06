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


    private EnemyScript[] RespawnableEnemies;
    [SerializeField]private GameObject SoulscratePrefab;
    [SerializeField]private PlayerScript PlayerReference;


    private void Start()
    {
        RespawnableEnemies = (FindObjectsByType<EnemyScript>(FindObjectsSortMode.None));
    }


    public void RespawnList()
    {
        foreach (EnemyScript Enemy in RespawnableEnemies)
        {
            Enemy.Respawn();
        }
    }

    public void SpawnSoulsCrate() 
    {
        Debug.Log("Try to spawn SoulsCrate");
         GameObject SoulsCrate = Instantiate(SoulscratePrefab, PlayerReference.transform.position, Quaternion.identity);
        Debug.Log("SoulsCrate spawned");
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn Player");
        //PlayerReference.Respawn();
    }
}
