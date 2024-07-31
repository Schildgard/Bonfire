using System;
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


    [SerializeField] private GameObject[] enemies;
    public GameObject[] Enemies => enemies;

    [SerializeField] private EnemyData[] EnemyDataArray;


    [SerializeField] private GameObject SoulscratePrefab;
    [SerializeField] private PlayerScript PlayerReference;
    [SerializeField] private Transform PlayerSpawnPosition;

    [SerializeField] private GameEvent UpdateEnemyEvent;




    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyDataArray = new EnemyData[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyDataArray[i].EnemyID = enemies[i].GetComponent<EnemyScript>().EnemyID;
            EnemyDataArray[i].Position = enemies[i].transform.position;
            EnemyDataArray[i].Rotation = enemies[i].transform.rotation;
        }
        UpdateEnemyEvent.Raise();

    }

    public void SetPlayerRespawnPoint()
    {
        PlayerSpawnPosition.position = PlayerReference.transform.position;
    }

    public void RespawnList()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
        for (int i = 0; i < EnemyDataArray.Length; i++)
        {
            enemies[i] = RespawnEnemy(EnemyDataArray[i].EnemyID, EnemyDataArray[i].Position, EnemyDataArray[i].Rotation);
        }
        UpdateEnemyEvent.Raise();
    }

    public void SpawnSoulsCrate()
    {
        Instantiate(SoulscratePrefab, PlayerReference.transform.position, Quaternion.identity);
    }

    public void RespawnPlayer()
    {
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
