using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class WaterSpell : MonoBehaviour
{
    [SerializeField] private Material EffectMaterial;
    //[SerializeField] private VisualEffect VFX;

    private void Awake()
    {
        this.transform.parent = null;
    }


    private void OnTriggerEnter(Collider _target)
    {
        var wettableTarget = _target.GetComponent<IWetable>();
        if (wettableTarget == null) return;

        wettableTarget.GetWet();

        // Change Materials


        var targetRenderer = _target.GetComponentInChildren<SkinnedMeshRenderer>();
        if (targetRenderer.materials.Length < 2) // Indicator if the the Second Material, which is the Electrify Material, already has been added or not
        {
            var Condition = targetRenderer.gameObject.AddComponent<EffectCondition_Wet>();

            targetRenderer.materials = new Material[] { Condition.OriginalMaterial[0], EffectMaterial }; ;
        }
        else
        {
            var EnemyCondition = targetRenderer.gameObject.GetComponent<EffectCondition_Wet>();
            EnemyCondition.duration = EnemyCondition.maxduration;
            Debug.Log(_target.name + "has already been wettified");
        }
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
