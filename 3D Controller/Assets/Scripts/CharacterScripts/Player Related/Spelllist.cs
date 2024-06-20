using System.Collections.Generic;
using UnityEngine;

public class Spelllist : MonoBehaviour
{

    private Animator Animator;
    private int spellIndex;
    public List<SO_Spell> Spells;

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
        spellIndex = _index;

        if (Spells[spellIndex] == null )
        {
            Debug.Log("No Spell Attached");
            return;
        }
        GetSpellSpawnPosition(spellIndex);

        if (Spells[spellIndex].alernativeCastAnimation)
        {
            Animator.SetTrigger("Cast Buff");
        }
        else
        {
            Animator.SetTrigger("Cast Spell");
        }


    }

    private void GetSpellSpawnPosition(int _index)
    {
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
                Debug.Log("No spellTypeIndex");
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
