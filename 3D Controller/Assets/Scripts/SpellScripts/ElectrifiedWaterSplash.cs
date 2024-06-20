using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedWaterSplash : MonoBehaviour
{
    private void OnTriggerEnter(Collider _target)
    {
        var wettableTarget = _target.GetComponent<IWetable>();
        if (wettableTarget == null) return;

        wettableTarget.GetWet();
        IElectrilizable electrilizableTarget = _target.GetComponentInChildren<IElectrilizable>();
        if (electrilizableTarget == null)
        {
            Debug.Log("not electrilizable");
        }
        electrilizableTarget.Electrify();

    }
}
