using UnityEngine;

public class EnemySpottingManager : MonoBehaviour
{
 private IAggroable[] Enemies;

    [SerializeField] private EnemySpawnManager EnemySpawnManager;

    private void Update()
    {
        // TO DO: Check if calculation is handled when Enemy is dead.
        foreach(var enemy in Enemies) 
        {
            enemy.CheckAggressiveBehaviour();
        }

    }

    public void UpdateEnemiyList()
    {
        Enemies = new IAggroable[EnemySpawnManager.Enemies.Length];

        for(int i = 0; i < EnemySpawnManager.Enemies.Length; i++)
        {
            Enemies[i] = EnemySpawnManager.Enemies[i].GetComponent<IAggroable>();
        }
    }

}