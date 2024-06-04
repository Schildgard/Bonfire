using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectCondition_Wet : StatusEffect, IElectrilizable
{


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

    public void Electrify(Material _effectMaterial)
    {
        var targetRenderer = GetComponent<SkinnedMeshRenderer>();
        if (targetRenderer.materials.Length <= 2) // Indicator if the the Second Material, which is the Electrify Material, already has been added or not
        {
            var Condition = targetRenderer.gameObject.AddComponent<EffectCondition_Lightning>();

            targetRenderer.materials = new Material[] { Condition.OriginalMaterial[0], _effectMaterial }; ;
        }
        else
        {
            var EnemyCondition = targetRenderer.gameObject.GetComponent<EffectCondition_Lightning>();
            EnemyCondition.duration = EnemyCondition.maxduration;
            Debug.Log(gameObject.name + "has already been electrified");
        }
    }

    public void Electrify(Vector3 _position)
    {
        Debug.Log("This Object needs a Material Parameter but you tried to implement a Vector3");
    }
}
