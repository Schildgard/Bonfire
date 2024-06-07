using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    public List<AbilityBarSlot> AbilityBarSlots;

    public GameObject SlotItemPrefab;


    private void Start()
    {
        InitializeSlots();
    }

    public SO_Spell GetAbilityFromSlot(int _index)
    {
        return AbilityBarSlots[_index].Spell;

    }


    private void InitializeSlots()
    {
        foreach (var SlotItem in AbilityBarSlots)
        {
            if (SlotItem.Spell == null)
            {
                continue;
            }
            CreateSlotItem(SlotItem, SlotItem.Spell);

        }
    }

    private void CreateSlotItem(AbilityBarSlot _abilityBarSlot, SO_Spell _spell)
    {
        var slotItem = Instantiate(SlotItemPrefab, _abilityBarSlot.transform);
         slotItem.GetComponent<Image>().sprite = _spell.SpellIcon;
    }
}
