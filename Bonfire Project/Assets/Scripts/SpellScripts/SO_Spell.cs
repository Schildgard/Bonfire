using UnityEngine;

[CreateAssetMenu(menuName = "Spell")]
public class SO_Spell : ScriptableObject
{


    public string spellName;
    public float spellDuration;
    public GameObject spellPrefab;
    public bool alernativeCastAnimation;

    public Sprite SpellIcon;
    [Range(0,2)]
    public int spellTypeIndex; //SpellTyoe Index specifies from which Transform the Spell instantiates.

    public GameObject spellCastAnimEffect;

}
