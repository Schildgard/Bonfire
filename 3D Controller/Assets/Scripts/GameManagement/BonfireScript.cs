using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireScript : MonoBehaviour
{

    public GameEvent RestEvent;

    [SerializeField]private GameObject ToolTip;
    private bool bonfireActivated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && bonfireActivated) 
        {
            RestEvent.Raise();
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 7) 
        {
            ToolTip.SetActive(true);
            bonfireActivated = true;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            ToolTip.SetActive(false);
            bonfireActivated=false;
        }
    }
}
