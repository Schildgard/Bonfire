using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spelllist : MonoBehaviour
{
    private Animator Animator;

    public List<SO_Spell> Spells;

    [SerializeField] private Transform ProjectileTransform;
    [SerializeField] private Transform BuffTransform;
    [SerializeField] private Transform AOETransform;

    private Transform SpawnPosition;

    private int spellIndex;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    public void CastSpell(int _index)
    {
        spellIndex = _index;
        if (Spells[_index].alernativeCastAnimation)
        {
            Animator.SetTrigger("Cast Buff");
        }
        else 
        {
            Animator.SetTrigger("Cast Spell");
        }


        switch (Spells[_index].spellTypeIndex)
        {
            case 0:
                SpawnPosition = ProjectileTransform;
                break;
            case 1:
                SpawnPosition = BuffTransform;
                break;
            case 2:
                SpawnPosition = AOETransform;
                break;
            default:
                break;
        }
    }

    public void InstantiateSpell()
    {
        GameObject SpellObject = Instantiate(Spells[spellIndex].spellPrefab, SpawnPosition);


        Destroy(SpellObject, Spells[spellIndex].spellDuration);
        spellIndex = 0;
    }

}




[Serializable]
public class Spell
{
    public SO_Spell SpellObject;
    //public Transform SpawnTransform;  


}
