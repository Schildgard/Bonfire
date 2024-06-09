using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropdownObject : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Enter On Drop");
        DraggableObject enteringObject = eventData.pointerDrag.GetComponent<DraggableObject>();


        AbilityBarSlot enteringObjectSlot = enteringObject.TargetSlot.GetComponent<AbilityBarSlot>();
        Debug.Log("Get OnDrop Variables");

        SwapAbility(enteringObjectSlot);

        if (transform.childCount == 0)
        {
            enteringObject.TargetSlot = this.transform;
            Debug.Log("OnDrop Done successfull");
        }
        else 
        {
            Debug.Log("NIN");
        }

    }



    private void SwapAbility(AbilityBarSlot _otherSlot)
    {
        var thisAbilityBarSlot = GetComponent<AbilityBarSlot>();

        SO_Spell tempAbility = thisAbilityBarSlot.Spell;

        thisAbilityBarSlot.Spell = _otherSlot.Spell;

        _otherSlot.Spell = tempAbility;

        Debug.Log("Switch happened");

    }

}
