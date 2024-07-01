
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Spawn1000Enemies : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private int maxEnemyCount = 1000;

    private List <Vector3> spawnPositions = new List<Vector3>();

    private Stopwatch stopwatch = new Stopwatch();


    // Start is called before the first frame update
    void Awake()
    {
        CalculateSpawnPositions();
        //UnityEngine.Debug.Log($"All {spawnPositions.Count} Positions have been calculated");


        stopwatch.Start();
        SpawnEnemies();
        stopwatch.Stop();
        //UnityEngine.Debug.Log($"Spawning all {spawnPositions.Count} Enemies took that much time: {stopwatch.Elapsed}");


        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CalculateSpawnPositions ()
    {
       // UnityEngine.Debug.Log("Start Calculating Spawn Position");
        for (int i = 0; i < maxEnemyCount; i++)
        {
            spawnPositions.Add(new Vector3(Random.Range(0, 501), 0, Random.Range(0, 501)));
        }
    }

    private void SpawnEnemies()
    {
       // UnityEngine.Debug.Log("Start Spawning Enemies");
        foreach (var position in spawnPositions)
        {
            Instantiate(Enemy, position, Quaternion.identity);
        }
        
    }
}
