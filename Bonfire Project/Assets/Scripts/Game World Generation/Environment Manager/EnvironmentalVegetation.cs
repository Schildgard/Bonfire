using UnityEngine;
[System.Serializable]
public class EnvironmentalVegetation

{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;

    private EnvironmentGenerator environmentGenerator;
    public EnvironmentGenerator EnvironmentGenerator => environmentGenerator;
    public Material Material => material;


    [Tooltip("Value Between -1 and 1. A higher Value reduces the spawnrate")]
    [SerializeField] private float threshold;

    [Tooltip("The maximum height in World Space where this item can spawn")]
    [SerializeField] private float maxYPosition;

    [SerializeField] private Vector3 scaleMultiplier;


    [SerializeField] private bool randomRotation;

    [Tooltip("Adds a small offset to each vegetation which makes it look a bit more natural")]
    [SerializeField] private bool randomizedOffset;

    [Tooltip("Minimum Flatness of the Vertex ti be a viable SpawnPosition. For spawning on flat areas a Value of 0.95f is recommended")]
    [SerializeField] private float minimumVertexFlatness = 0.95f;


    public EnvironmentGenerator InitializeGenerator(Mesh _mesh, Transform _planeTransform)
    {

        if (mesh == null)
        {
            environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, threshold, scaleMultiplier, randomRotation, randomizedOffset, maxYPosition, _planeTransform, minimumVertexFlatness);
            if (Material.enableInstancing == false) { Material.enableInstancing = true; }
        }
        else
        {
            environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, threshold, scaleMultiplier, mesh, randomRotation, randomizedOffset, maxYPosition, _planeTransform, minimumVertexFlatness);
            if (Material.enableInstancing == false) { Material.enableInstancing = true; }
        }

        return environmentGenerator;
    }
}
