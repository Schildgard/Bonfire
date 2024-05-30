using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class WaterSpell : MonoBehaviour
{
    [SerializeField] private Material EffectMaterial;
    [SerializeField] private VisualEffect VFX;



    private void OnTriggerEnter(Collider _target)
    {
        Debug.Log("Wet");
        var wettableTarget = _target.GetComponent<IWetable>();
            if (wettableTarget == null) return;



            var targetRenderer = _target.GetComponentInChildren<SkinnedMeshRenderer>();
            // Change Materials
            //Apply VFX
            VisualEffect vfx = Instantiate(VFX, _target.transform);

            vfx.SetSkinnedMeshRenderer("SkinnedMeshRenderer", targetRenderer);
            
        VFXPropertyBinder vfxPropertyBinder = vfx.AddComponent<VFXPropertyBinder>();
        //vfxPropertyBinder.add
            
      

    }
}
