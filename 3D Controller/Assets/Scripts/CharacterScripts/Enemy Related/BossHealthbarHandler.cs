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
            AudioManager.instance.ChangeBackGroundMusic(1); // Hat hier im Script nichts verloren. Ich sollte ein Event system dafür nutzen.
        }
        else if (!enemyDetectionScript.Detected && healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(false);
            AudioManager.instance.ChangeBackGroundMusic(0);

        }
    }

    public void DisableHealthCanvas() // WIrd das irgendwo aufgerufen? Anscheinend als Event irgendwo.
    {
        enemyDetectionScript.Detected = false;
        if (healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(false);
            AudioManager.instance.ChangeBackGroundMusic(0);
        }

        Destroy(this);
    }
}
