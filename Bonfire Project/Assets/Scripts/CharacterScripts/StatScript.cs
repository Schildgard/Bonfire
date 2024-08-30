using UnityEngine;

public class StatScript : MonoBehaviour
{
    //Player Name finds no relevance yet, but it is planned to be able to name the Character
    [SerializeField] private string playerName;
    [SerializeField] private float level;

    [SerializeField] private float strength =1;
    [SerializeField] private float vitality =1;
    [SerializeField] private float speed = 1;
    [SerializeField] private float defense = 1;

    [SerializeField] private float soulsValue;
    public float Strength { get{ return strength;} set { strength = value;} }
    public float Vitality {get{ return vitality;}  set{ vitality = value; }}
    public float Speed    {get{ return speed;}     set{ speed = value; }}
    public float Defense  { get { return defense;} set {defense = value; }}
    public float SoulsValue { get { return soulsValue;} set {  soulsValue = value; }}

    public string PlayerName { get { return name; } set {  name = value; } }

    public float Level { get { return level; } set { level = value; } }

}
