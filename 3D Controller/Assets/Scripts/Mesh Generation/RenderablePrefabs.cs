using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RenderablePrefabs 
{
    [SerializeField]private GameObject prefab;
    [SerializeField] private int maxPrefabCount;
    [SerializeField] private bool enableMaxCount;

    protected PrefabGenerator environmentGenerator;

    public GameObject Prefab => prefab;
    public PrefabGenerator EnvironmentGenerator => environmentGenerator;
    public int MaxPrefabCount => maxPrefabCount;



    [SerializeField] private float Threshold;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private bool randomRotation;
    [SerializeField] private bool randomizedOffset;


    public bool RandomRotation => randomRotation;

    public PrefabGenerator InitializeGenerator(Mesh _mesh)
    {
        environmentGenerator = new PrefabGenerator(_mesh, maxPrefabCount, enableMaxCount, Threshold, Offset, randomizedOffset);
        return environmentGenerator;
    }
}
