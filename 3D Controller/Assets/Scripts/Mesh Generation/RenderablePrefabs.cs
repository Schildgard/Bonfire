using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RenderablePrefabs 
{
    public GameObject Prefab;

    protected PrefabGenerator environmentGenerator;
    public PrefabGenerator EnvironmentGenerator => environmentGenerator;

    public EnvironmentalSettings environmentalSettings;

    public PrefabGenerator InitializeGenerator(Mesh _mesh)
    {
        environmentGenerator = new PrefabGenerator(_mesh, environmentalSettings);
        return environmentGenerator;
    }
}
