using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Environmental Settings", menuName = "Create New Environmental Settings")]
[System.Serializable]
public class EnvironmentalSettings : ScriptableObject
{

    public float Threshold;

    public Vector3 Offset;
    public Vector3 ScaleMultiplier;


}






[System.Serializable]
public class EnvironmentalSettingsLayer
{
    public string name;
    public EnvironmentalSettings EnvironmentalSettings;
}
