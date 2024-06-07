using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Spell")]
public class SO_Spell : ScriptableObject
{
    public string spellName;
    public float spellDuration;
    public GameObject spellPrefab;
    public bool alernativeCastAnimation;

    public Sprite SpellIcon;
}
