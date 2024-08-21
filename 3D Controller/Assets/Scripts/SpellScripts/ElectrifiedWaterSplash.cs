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
            return;
        }
        electrilizableTarget.Electrify();

    }
}
