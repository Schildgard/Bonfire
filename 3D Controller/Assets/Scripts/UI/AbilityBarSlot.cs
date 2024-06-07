using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBarSlot : MonoBehaviour
{

    [SerializeField]private SO_Spell spell;


    public SO_Spell Spell { get { return spell; } set { spell = value; } }
}
