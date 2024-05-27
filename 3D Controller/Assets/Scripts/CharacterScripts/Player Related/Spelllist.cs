using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spelllist : MonoBehaviour
{

    public List<Spell> Spells;



}

[Serializable]
public class Spell
{
    [SerializeField] private string name;
    public GameObject Prefab;
    [SerializeField] private Transform SpawnTransform;


}
