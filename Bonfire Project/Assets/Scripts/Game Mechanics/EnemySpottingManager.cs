using UnityEngine;

public class EnemySpottingManager : MonoBehaviour
{
    //The Spotting Manager handles the Enemies alarming each other when triggered.
    private IAggroable[] Enemies;
    [SerializeField] private RespawnManager EnemySpawnManager;

    private void Update()
    {
        foreach (var enemy in Enemies)
        {
            enemy.CheckAggressiveBehaviour();
        }
    }

    public void UpdateEnemiyList()
    {
        Enemies = new IAggroable[EnemySpawnManager.Enemies.Length];

        for (int i = 0; i < EnemySpawnManager.Enemies.Length; i++)
        {
            Enemies[i] = EnemySpawnManager.Enemies[i].GetComponent<IAggroable>();
        }
    }

}