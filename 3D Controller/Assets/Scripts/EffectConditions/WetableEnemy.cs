using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetableEnemy : MonoBehaviour, IWetable
{
    [SerializeField]private GameObject WetCondition;
    public Material ElectrifiedMaterial;


    public void GetWet()
    {

        WetCondition.SetActive(true);
    }

    public void GetDry() 
    {
        WetCondition.SetActive(false);
    }
}
