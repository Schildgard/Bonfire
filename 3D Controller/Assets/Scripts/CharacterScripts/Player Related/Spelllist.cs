using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spelllist : MonoBehaviour
{
    private Animator Animator;

    public List<Spell> Spells;

    private int spellIndex;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    public void CastSpell(int _index)
    {
        spellIndex = _index;
        if (Spells[_index].SpellObject.alernativeCastAnimation)
        {
            Animator.SetTrigger("Cast Buff");
        }
        else 
        {
            Animator.SetTrigger("Cast Spell");
        }
    }

    public void InstantiateSpell()
    {
        GameObject SpellObject = Instantiate(Spells[spellIndex].SpellObject.spellPrefab, Spells[spellIndex].SpawnTransform);
        Destroy(SpellObject, Spells[spellIndex].SpellObject.spellDuration);
        spellIndex = 0;
    }

}




[Serializable]
public class Spell
{
    public SO_Spell SpellObject;
    public Transform SpawnTransform;  


}
