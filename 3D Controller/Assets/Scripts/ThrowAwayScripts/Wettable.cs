using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wettable : MonoBehaviour, IWetable
{
    public GameObject WetCondition;
    public void GetWet()
    {

        WetCondition.SetActive(true);
    }

    public void GetDry() 
    {
        WetCondition.SetActive(false);
    }
}
