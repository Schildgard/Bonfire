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


    public void DisableHealthCanvas()
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
