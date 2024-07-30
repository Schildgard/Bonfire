using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject[] EnemyPrefabs;


    [SerializeField] private GameObject[] Enemies;

    [SerializeField] private EnemyData[] EnemyDataArray;


    [SerializeField] private GameObject SoulscratePrefab;
    [SerializeField] private PlayerScript PlayerReference;
    [SerializeField] private Transform PlayerSpawnPosition;




    private void Start()
    {
        //   RespawnableEnemies = (FindObjectsByType<EnemyScript>(FindObjectsSortMode.None));

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyDataArray = new EnemyData[Enemies.Length];

        for (int i = 0; i < Enemies.Length; i++)
        {
            EnemyDataArray[i].EnemyID = Enemies[i].GetComponent<EnemyScript>().EnemyID;
            EnemyDataArray[i].Position = Enemies[i].transform.position;
            EnemyDataArray[i].Rotation = Enemies[i].transform.rotation;
        }

    }

    public void SetPlayerRespawnPoint()
    {
        PlayerSpawnPosition.position = PlayerReference.transform.position;
    }

    public void RespawnList()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            Destroy(Enemies[i].gameObject);
        }
        for (int i = 0; i < EnemyDataArray.Length; i++)
        {
            Enemies[i] = RespawnEnemy(EnemyDataArray[i].EnemyID, EnemyDataArray[i].Position, EnemyDataArray[i].Rotation);

        }
    }

    public void SpawnSoulsCrate()
    {
        Debug.Log("Try to spawn SoulsCrate");
        Instantiate(SoulscratePrefab, PlayerReference.transform.position, Quaternion.identity);
        Debug.Log("SoulsCrate spawned");
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn Player");
        PlayerReference.Respawn();
        PlayerReference.transform.position = PlayerSpawnPosition.position;
    }

    public GameObject RespawnEnemy(int _enemyID, Vector3 _position, Quaternion _rotation)
    {
      GameObject Enemy = Instantiate(EnemyPrefabs[_enemyID], _position, _rotation);
        return Enemy;
    }
}


[Serializable]
public struct EnemyData
{
    public int EnemyID;
    public Vector3 Position;
    public Quaternion Rotation;
}
