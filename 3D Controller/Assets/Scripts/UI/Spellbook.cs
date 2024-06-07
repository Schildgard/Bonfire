using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spellbook : MonoBehaviour
{
    [SerializeField]private List<AbilityBarSlot> LearnedSpells;



    private void Awake()
    {
        foreach (AbilityBarSlot Ability in LearnedSpells)
        {
            if (Ability.Spell == null) continue;
            Ability.GetComponent<Image>().sprite = Ability.Spell.SpellIcon;
        }
    }

}
