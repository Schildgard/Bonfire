using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectCondition_Wet : StatusEffect, IElectrilizable
{

    private Material ElectrifiedMaterial;

    protected override void Awake()
    {
        maxduration = 20;
        base.Awake();
    }

    protected override void Update()
    {
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            var WetConditionVariable = GetComponentInParent<IWetable>();
            WetConditionVariable.GetDry();
            SkinnedMeshRenderer.materials = OriginalMaterial;
            Destroy(this);
            Destroy(AudioSource);

        }
    }

    public void Electrify()
    {
        var Wetable = GetComponentInParent<WetableEnemy>();
        ElectrifiedMaterial = Wetable.ElectrifiedMaterial;


        
        var targetRenderer = GetComponent<SkinnedMeshRenderer>();
        if (targetRenderer.materials.Length <= 2) // Indicator if the the Second Material, which is the Electrify Material, already has been added or not
        {
            var Condition = targetRenderer.gameObject.AddComponent<EffectCondition_Lightning>();

            targetRenderer.materials = new Material[] { Condition.OriginalMaterial[0], ElectrifiedMaterial }; ;
        }
        else
        {
            var EnemyCondition = targetRenderer.gameObject.GetComponent<EffectCondition_Lightning>();
            EnemyCondition.duration = EnemyCondition.maxduration;
            Debug.Log(gameObject.name + "has already been electrified");
        }
    }
}
