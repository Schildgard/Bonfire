using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AreaCollection: MonoBehaviour
{
    [SerializeField] private Area[] areas;
    [SerializeField] private List<GameObject> prefabsInScene;

    public List<GameObject> PrefabsInScene => prefabsInScene;
    public Area[] Areas => areas;
}
