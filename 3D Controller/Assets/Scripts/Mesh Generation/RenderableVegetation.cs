using UnityEngine;
[System.Serializable]
public class RenderableVegetation
{
    public Material Material;

    private EnvironmentGenerator environmentGenerator;
    public EnvironmentGenerator EnvironmentGenerator => environmentGenerator;



    [SerializeField] private float Threshold;
    [SerializeField] private float maxYPosition;

    [SerializeField] private Vector3 ScaleMultiplier;

    [SerializeField] private Mesh mesh;

    [SerializeField] private bool randomRotation;
    [SerializeField] private bool randomizedOffset;

    [Tooltip("Minimum Flatness of the Vertex ti be a viable SpawnPosition. For spawning on flat areas a Value of 0.95f is recommended")]
    [SerializeField] private float minimumVertexFlatness = 0.95f;


    public EnvironmentGenerator InitializeGenerator(Mesh _mesh, Transform _planeTransform)
    {

        if (mesh == null)
        {
            environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, Threshold, ScaleMultiplier, randomRotation, randomizedOffset, maxYPosition, _planeTransform, minimumVertexFlatness);
            if (Material.enableInstancing == false) { Material.enableInstancing = true; }
        }
        else
        {
            environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, Threshold, ScaleMultiplier, mesh, randomRotation, randomizedOffset, maxYPosition, _planeTransform, minimumVertexFlatness);
            if (Material.enableInstancing == false) { Material.enableInstancing = true; }
        }

        return environmentGenerator;
    }
}
