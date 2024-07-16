using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RenderablePrefabs 
{
    [SerializeField]private GameObject prefab;
    [SerializeField]private EnvironmentalSettings environmentalSettings;
    [SerializeField] private int maxPrefabCount;
    [SerializeField] private bool enableMaxCount;

    protected PrefabGenerator environmentGenerator;

    public GameObject Prefab => prefab;
    public PrefabGenerator EnvironmentGenerator => environmentGenerator;

    public int MaxPrefabCount => maxPrefabCount;


    public PrefabGenerator InitializeGenerator(Mesh _mesh)
    {
        environmentGenerator = new PrefabGenerator(_mesh, environmentalSettings, maxPrefabCount, enableMaxCount);
        return environmentGenerator;
    }
}
