using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ElectrifyableSurface : MonoBehaviour, IElectrilizable
{
    public GameObject ElectrifiedSurfaceVFX;


    public void Electrify()
    {

      ElectrifiedSurfaceVFX.SetActive(true);

    }
}
