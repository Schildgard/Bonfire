using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviourScript : MonoBehaviour, IElectrilizable
{

    public GameObject ElectrifiedVFX;
    public void Electrify()
    {
        Debug.Log("Water electrified");
        //Instantiate(ElectrifiedVFX)

    }
}
