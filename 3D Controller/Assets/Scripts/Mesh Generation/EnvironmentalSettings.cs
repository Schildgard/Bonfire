using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Environmental Settings", menuName = "Create New Environmental Settings")]
public class EnvironmentalSettings : ScriptableObject
{

    public float Threshold;

    public Vector3 Offset;
    public Vector3 ScaleMultiplier;
}
