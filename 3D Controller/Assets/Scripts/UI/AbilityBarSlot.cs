using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityBarSlot : MonoBehaviour, IDropHandler
{

    [SerializeField] private SO_Spell spell;
    [SerializeField] private Spelllist Spelllist;

    [SerializeField]private int spelllistIndex;


    public SO_Spell Spell { get { return spell; } set { spell = value; } }

    private void Start()
    {
        Spelllist = GameObject.FindObjectOfType<Spelllist>();

        if (Spelllist.Spells.Count <= spelllistIndex)
        {
            Spelllist.Spells.Add(spell);
        }
        else
        Spelllist.Spells[spelllistIndex] = spell;

    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableObject enteringObject = eventData.pointerDrag.GetComponent<DraggableObject>();
        AbilityBarSlot enteringObjectSlot = enteringObject.TargetSlot.GetComponent<AbilityBarSlot>();

        SwapAbility(enteringObjectSlot);

        if (transform.childCount == 0)
        {
            enteringObject.TargetSlot = this.transform;
        }
        else
        {
            DraggableObject existingObject = eventData.pointerEnter.GetComponent<DraggableObject>();
            SwapSlots(enteringObject, existingObject);
        }
    }
    private void SwapAbility(AbilityBarSlot _otherSlot)
    {
        SO_Spell tempAbility = spell;
        spell = _otherSlot.Spell;
        _otherSlot.Spell = tempAbility;

        Spelllist.Spells[spelllistIndex] = spell;
        Spelllist.Spells[_otherSlot.spelllistIndex] = tempAbility;

    }

    private void SwapSlots(DraggableObject _enteringObject, DraggableObject _existingObject) //RENAME
    {
        Transform tempTargetSlot = _enteringObject.TargetSlot;
        _enteringObject.TargetSlot = _existingObject.TargetSlot;
        _existingObject.MoveSlotItem(tempTargetSlot);
    }

    public void AssignNewAbilityToEmptySlot(SO_Spell _spell)
    {
        Spelllist.Spells[spelllistIndex] = _spell;
    }
}
