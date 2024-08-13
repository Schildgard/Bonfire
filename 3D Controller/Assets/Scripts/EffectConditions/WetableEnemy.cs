using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WetableEnemy : MonoBehaviour, IWetable
{
    [SerializeField] private GameObject ObjectToPlaceEffectCondition; //Target Child on which the EffectConditiongetsAdded
    [SerializeField]private GameObject WetCondition;
    private Animator animator;

    public Material ElectrifiedMaterial; // Used by EffectCondition - Wet (which is neccessary since EffectCondition_Wet is added via Runtime, and contains no serializable fields
    public Material WetMaterial;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void GetWet()
    {
        WetCondition.SetActive(true); // Can probably be handled by EffectConditionScript_Wet as well
        ObjectToPlaceEffectCondition.AddComponent<EffectCondition_Wet>();

    }

    public void GetDry() 
    {
        WetCondition.SetActive(false); // Can probably be handled by EffectConditionScript_Wet as well
        animator.SetBool("Electrified", false);
    }
}
