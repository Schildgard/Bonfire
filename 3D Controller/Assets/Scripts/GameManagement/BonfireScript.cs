using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireScript : MonoBehaviour
{

    public GameEvent RestEvent;

    [SerializeField]private GameObject Tooltip;
    [SerializeField]private bool bonfireActivated;


    // Update is called once per frame
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
            Tooltip.SetActive(true);
            bonfireActivated = true;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            Tooltip.SetActive(false);
            bonfireActivated=false;
        }
    }
}
