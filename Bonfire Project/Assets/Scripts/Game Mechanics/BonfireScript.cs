using UnityEngine;

public class BonfireScript : MonoBehaviour
{
    // Bonfire Event serves a variety of tasks such as trigger the level up screen, respawn enemies or refill player health.
    public GameEvent RestEvent;

    [SerializeField]private GameObject tooltip;
    [SerializeField]private bool bonfireActivated;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && bonfireActivated)
        {
            RestEvent.Raise();
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 7) 
        {
            tooltip.SetActive(true);
            bonfireActivated = true;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            tooltip.SetActive(false);
            bonfireActivated=false;
        }
    }
}
