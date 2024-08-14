using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AreaCollection : MonoBehaviour
{
    [SerializeField] private Area[] areas;
    [SerializeField] private List<GameObject> prefabsInScene;
    [SerializeField] private bool renderInstancedMeshes;



    public List<GameObject> PrefabsInScene => prefabsInScene;
    public Area[] Areas => areas;
    public bool RenderInstancedMeshes => renderInstancedMeshes;

    private EnvironmentManager environmentManager;

    private void OnEnable()
    {
        environmentManager = GetComponent<EnvironmentManager>();
    }
    public void GenerateEnvironment()
    {
        environmentManager.Initialize();
    }

    public void KeepPrefabsInScene()
    {
        environmentManager.ClearPrefabList();
    }

    public void RemoveEnvironmentFromScene()
    {
        environmentManager.RemoveEnvironmentPrefabs();
    }
}
