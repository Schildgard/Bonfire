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

    private Spelllist Spelllist;



    private void Start()
    {
        Spelllist = GetComponentInParent<Spelllist>();
        InitializeSlots();
    }





    private void InitializeSlots()
    {
        foreach (var Ability in AbilityBarSlots)
        {
            if (Ability.Spell == null)
            {
                Debug.Log(Ability.name + " Spell is null");
                continue;
            }
            Debug.Log("Create SlotItem for" + Ability);
            CreateSlotItem(Ability, Ability.Spell);
            Debug.Log("Now Adding to List");
            Spelllist.Spells.Add(Ability.Spell);
            Debug.Log("Success");
        }
    }

    private void CreateSlotItem(AbilityBarSlot _abilityBarSlot, SO_Spell _spell)
    {
        var slotItem = Instantiate(SlotItemPrefab, _abilityBarSlot.transform);
        slotItem.GetComponent<Image>().sprite = _spell.SpellIcon;
    }

    public SO_Spell GetAbilityFromSlot(int _index)
    {
        return AbilityBarSlots[_index].Spell;

    }
}
