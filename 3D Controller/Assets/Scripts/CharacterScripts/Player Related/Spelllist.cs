using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Spelllist : MonoBehaviour
{
    private Animator Animator;

    public List<Spell> Spells;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    public void CastSpell(int _index)
    {
        if (Spells[_index].useAltCastAnimation)
        {
            Animator.SetTrigger("Cast Buff");
        }
        else 
        {
            Animator.SetTrigger("Cast Spell");
        }
        Instantiate(Spells[_index].Prefab, Spells[_index].SpawnTransform);
    }


}

[Serializable]
public class Spell
{
    [SerializeField] private string name;
    public GameObject Prefab;
    public Transform SpawnTransform;  
    public bool useAltCastAnimation;


}
