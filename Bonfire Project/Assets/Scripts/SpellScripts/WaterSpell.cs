using UnityEngine;

public class WaterSpell : MonoBehaviour
{
    private void Awake()
    {
        this.transform.parent = null;
        transform.rotation = Quaternion.identity;
    }


    private void OnTriggerEnter(Collider _target)
    {
        var wettableTarget = _target.GetComponent<IWetable>();
        if (wettableTarget == null) return;

        wettableTarget.GetWet();

    }


    private void OnTriggerStay(Collider _target)
    {
        var targetRenderer = _target.GetComponentInChildren<SkinnedMeshRenderer>();
        if (targetRenderer == null) return;

        var WetCondition = targetRenderer.gameObject.GetComponent<EffectCondition_Wet>();
        if (WetCondition == null) return;
        WetCondition.duration = WetCondition.maxduration;
    }
}
