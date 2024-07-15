using UnityEngine;
[System.Serializable]
public class RenderableVegetation
{
    public Material Material;
    [Range(0, 1)]
    public int RenderMode;

    private EnvironmentGenerator environmentGenerator;
    public EnvironmentGenerator EnvironmentGenerator => environmentGenerator;



    [SerializeField]private float Threshold;

    [SerializeField]private Vector3 Offset;
    [SerializeField]private Vector3 ScaleMultiplier;

    [SerializeField]private Mesh mesh;

    [SerializeField] private bool randomRotation;
    [SerializeField] private bool randomizedOffset;


    public EnvironmentGenerator InitializeGenerator(Mesh _mesh)
    {

        if (mesh == null)
        {
            switch (RenderMode)
            {
                case 0:
                    environmentGenerator = new VegetationGenerator(_mesh, Material, Threshold, Offset, ScaleMultiplier);
                    break;
                case 1:
                    environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, Threshold, Offset, ScaleMultiplier, randomRotation, randomizedOffset);
                    if (Material.enableInstancing == false) { Material.enableInstancing = true; }
                    break;
                default:
                    environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, Threshold, Offset, ScaleMultiplier, randomRotation, randomizedOffset);
                    if (Material.enableInstancing == false) { Material.enableInstancing = true; }
                    break;
            }

        }
        else
        {

            switch (RenderMode)
            {
                case 0:
                    environmentGenerator = new VegetationGenerator(_mesh, Material, Threshold, Offset, ScaleMultiplier);
                    break;
                case 1:
                    environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, Threshold, Offset, ScaleMultiplier, mesh, randomRotation, randomizedOffset);
                    if (Material.enableInstancing == false) { Material.enableInstancing = true; }
                    break;
                default:
                    environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, Threshold, Offset, ScaleMultiplier, mesh, randomRotation, randomizedOffset);
                    if (Material.enableInstancing == false) { Material.enableInstancing = true; }
                    break;
            }
        }

        return environmentGenerator;
    }
}
