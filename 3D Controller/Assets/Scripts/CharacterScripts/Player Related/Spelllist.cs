using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spelllist : MonoBehaviour
{

    private Animator Animator;
    //private List<SO_Spell> spells;
    private int spellIndex;
    public List<SO_Spell> Spells; //{ get { return spells; } set { spells = value; } }

    [SerializeField] private Transform ProjectileTransform;
    [SerializeField] private Transform BuffTransform;
    [SerializeField] private Transform AOETransform;

    private Transform SpawnPosition;


    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }



    public void CastSpell(int _index)
    {
        GetSpellIndexAndSpawnPosition(_index);

        if (Spells[spellIndex].alernativeCastAnimation)
        {
            Animator.SetTrigger("Cast Buff");
        }
        else
        {
            Animator.SetTrigger("Cast Spell");
        }


    }

    private void GetSpellIndexAndSpawnPosition(int _index)
    {


        spellIndex = _index;

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
