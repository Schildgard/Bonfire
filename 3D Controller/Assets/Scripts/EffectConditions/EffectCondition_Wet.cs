using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class EffectCondition_Wet : StatusEffect, IElectrilizable
{

    private Material ElectrifiedMaterial;
    private Material WetMaterial;

    public bool Electrified;

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
        }
    }

    public void Electrify()
    {
        var Wetable = GetComponentInParent<WetableEnemy>();
        ElectrifiedMaterial = Wetable.ElectrifiedMaterial;


        // Material Array Check can probably be replaced with a simple bool which checks if the Target ist electrified.
        if (!Electrified)
        {
            var Condition = SkinnedMeshRenderer.gameObject.AddComponent<EffectCondition_Lightning>();
            //  SkinnedMeshRenderer.materials = new Material[] { Condition.OriginalMaterial[0], ElectrifiedMaterial };
            SkinnedMeshRenderer.materials = PrepareNewMaterialArray(OriginalMaterial, ElectrifiedMaterial);

            Electrified = true;
        }
        else
        {
            var lightningCondition = SkinnedMeshRenderer.gameObject.GetComponent<EffectCondition_Lightning>();
            lightningCondition.duration = lightningCondition.maxduration;
            Debug.Log(gameObject.name + "has already been electrified");
        }
    }

    public void Electrify(Vector3 _hitpoint)
    {

        Debug.Log("Call of Electrified Method used a Vector3 as Input, which is not intended for this Effect." +
            " Go the Scripit in which the Electrify Method is called and remove the Vector3 Paramater.");
        Electrify();
    }


    private void OnEnable()
    {
        var Wetable = GetComponentInParent<WetableEnemy>();
        WetMaterial = Wetable.WetMaterial;

        Debug.Log($"Original Materials {OriginalMaterial.Length}");


        SkinnedMeshRenderer.materials = PrepareNewMaterialArray(OriginalMaterial, WetMaterial);
        //SkinnedMeshRenderer.materials = new Material[] { OriginalMaterial[0], WetMaterial }; ;
    }

    private Material[] PrepareNewMaterialArray(Material[] _inputArray, Material _statusMaterial)
    {
        Material[] newArray = new Material[_inputArray.Length +1];
        Debug.Log($"New Mesh Materials {newArray.Length}");
        for (int i = 0; i < _inputArray.Length; i++)
        {
            newArray[i] = _inputArray[i];
        }
        Debug.Log("Ore Test");

        newArray[newArray.Length-1] = _statusMaterial;
        Debug.Log("Test");

        return newArray;
    }
}
