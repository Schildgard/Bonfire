using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    [SerializeField] private List<AbilityBarSlot> AbilityBarSlots;

    [SerializeField] private GameObject SlotItemPrefab;

    //[SerializeField]private Spelllist Spelllist;



    private void Start()
    {
        InitializeSlots();
    }


    private void InitializeSlots()
    {
        foreach (var Ability in AbilityBarSlots)
        {
            if (Ability.Spell == null)
            {
                continue;
            }
            CreateSlotIcon(Ability, Ability.Spell);
            //Spelllist.Spells.Add(Ability.Spell);
        }
    }

    private void CreateSlotIcon(AbilityBarSlot _abilityBarSlot, SO_Spell _spell)
    {
        var slotItem = Instantiate(SlotItemPrefab, _abilityBarSlot.transform);
        slotItem.GetComponent<Image>().sprite = _spell.SpellIcon;
    }
}
