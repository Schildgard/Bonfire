using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (fileName = "Dialogue", menuName ="Create New Dialogue")]
public class Dialogue :ScriptableObject
{

    public string DialogueTitle;

    public List<TMP_Text> Lines;

    public GameEvent dialogueEvent;



}
