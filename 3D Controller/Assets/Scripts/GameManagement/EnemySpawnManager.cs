using System;
using System.Collections.Generic;
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

    #region Enemies
    [SerializeField] private GameObject[] EnemyPrefabs; // Stores the Prefabs of all Enemies so they can be respawned after destroy
    [SerializeField] private GameObject[] enemies; // stores all enemies in Scene
    public GameObject[] Enemies => enemies;
    [SerializeField] private List<EnemyData> EnemyDataArray; //stores important informations of enemies in Scene, so they can respawn on their correct Position
    [SerializeField] private GameEvent UpdateEnemyEvent;
    #endregion


    [SerializeField] private GameObject SoulscratePrefab;


    #region Player
    [SerializeField] private Transform PlayerSpawnPosition;
    [SerializeField] private PlayerScript PlayerReference;
    #endregion


    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        EnemyDataArray = new List<EnemyData>();

        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyDataArray.Add(new EnemyData(enemies[i].GetComponent<EnemyScript>().EnemyID, enemies[i].transform.position, enemies[i].transform.rotation));
        }
        UpdateEnemyEvent.Raise();


    }

    public void SetPlayerRespawnPoint()
    {
        PlayerSpawnPosition.position = PlayerReference.transform.position;
    }

    public void RespawnEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
        enemies = new GameObject[EnemyDataArray.Count];
        for (int i = 0; i < EnemyDataArray.Count; i++)
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
        PlayerReference.gameObject.transform.position = new Vector3(PlayerSpawnPosition.position.x, PlayerSpawnPosition.position.y, PlayerSpawnPosition.position.z);
    }

    public GameObject RespawnEnemy(int _enemyID, Vector3 _position, Quaternion _rotation)
    {
        GameObject Enemy = Instantiate(EnemyPrefabs[_enemyID], _position, _rotation);
        return Enemy;
    }

    public void RemoveBossFromRespawnList()
    {
        int indexToRemove = 0;
        for (int i = 0; i < EnemyDataArray.Count; i++)
        {
            if (EnemyDataArray[i].EnemyID == 2)
            {
                indexToRemove = i;
            }
        }
        EnemyDataArray.Remove(EnemyDataArray[indexToRemove]);
        UpdateEnemyEvent.Raise();
    }
}


[Serializable]
public class EnemyData
{
    public EnemyData(int _iD, Vector3 _position, Quaternion _rotation)
    {
        EnemyID = _iD;
        Position = _position;
        Rotation = _rotation;
    }
    public int EnemyID;
    public Vector3 Position;
    public Quaternion Rotation;
}
