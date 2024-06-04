using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterBehaviourScript : MonoBehaviour, IElectrilizable
{


    public GameObject ElectrifiedSurfaceVFX;


    public void Electrify(Material _material)
    {
        
        Debug.Log("This Object needs a Vector3 Parameter, but you implemented a Material instead.");

    }

    public void Electrify(Vector3 _position)
    {
        GameObject vfx = Instantiate(ElectrifiedSurfaceVFX, _position, Quaternion.Euler(-90, 0, 0));
    }
}
