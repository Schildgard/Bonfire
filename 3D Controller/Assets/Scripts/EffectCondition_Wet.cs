using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectCondition_Wet : StatusEffect, IElectrilizable
{

    protected override void Awake()
    {
        maxduration = 10;
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
}
