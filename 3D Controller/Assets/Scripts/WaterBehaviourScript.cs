using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterBehaviourScript : MonoBehaviour, IElectrilizable
{

    public GameObject ElectrifiedSurfaceVFX;
    public void Electrify(Vector3 _position)
    {
        Debug.Log("Water electrified");
        GameObject vfx = Instantiate(ElectrifiedSurfaceVFX, _position, Quaternion.Euler(-90, 0, 0));
        //Instantiate(ElectrifiedVFX)

    }
}
