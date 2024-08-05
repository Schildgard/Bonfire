using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthbarHandler : MonoBehaviour
{
    private EnemyDetectionScript enemyDetectionScript;
    [SerializeField] private GameObject healthCanvas;

    private void Start()
    {
        enemyDetectionScript = GetComponent<EnemyDetectionScript>();
    }
    private void Update()
    {
        if (enemyDetectionScript.Detected && !healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(true);
        }
        else if (!enemyDetectionScript.Detected && healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(false);
        }
    }
}
