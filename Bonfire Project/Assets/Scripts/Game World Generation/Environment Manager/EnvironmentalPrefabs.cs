using UnityEngine;
[System.Serializable]
public class EnvironmentalPrefabs

{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxPrefabCount;
    [SerializeField] private bool enableMaxCount;

    protected PrefabGenerator environmentGenerator;

    [SerializeField] private float threshold;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool randomRotation;
    [SerializeField] private bool randomizedOffset;
    [SerializeField] private float maxYPosition;

    public GameObject Prefab => prefab;
    public PrefabGenerator EnvironmentGenerator => environmentGenerator;
    public int MaxPrefabCount => maxPrefabCount;
    public bool RandomRotation => randomRotation;

    public PrefabGenerator InitializeGenerator(Mesh _mesh, Transform _planeTransform)
    {
        environmentGenerator = new PrefabGenerator(_mesh, maxPrefabCount, enableMaxCount, threshold, offset, randomizedOffset, _planeTransform, maxYPosition);
        return environmentGenerator;
    }
}
