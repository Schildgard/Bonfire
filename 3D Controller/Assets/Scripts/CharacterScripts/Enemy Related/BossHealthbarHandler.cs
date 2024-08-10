using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthbarHandler : MonoBehaviour
{
    private EnemyDetectionScript enemyDetectionScript;
    [SerializeField] private GameObject healthCanvas;
    [SerializeField] private GameEvent bossTriggerEvent;
    [SerializeField] private GameEvent bossDetriggerEvent;

    private void Start()
    {
        enemyDetectionScript = GetComponent<EnemyDetectionScript>();
    }
    private void Update()
    {
        if (enemyDetectionScript.Detected && !healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(true);
            bossTriggerEvent.Raise();
        }
        else if (!enemyDetectionScript.Detected && healthCanvas.activeSelf)
        {
            healthCanvas.SetActive(false);
            bossDetriggerEvent.Raise();

        }
    }

    private void OnDestroy()
    {
        //bossDetriggerEvent.Raise();
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
