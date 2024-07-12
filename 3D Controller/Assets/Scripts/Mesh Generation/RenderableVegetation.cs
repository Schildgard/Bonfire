using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RenderableVegetation
{
    public Material Material;
    [Range(1, 2)]
    public int RenderMode;

    private EnvironmentGenerator environmentGenerator;
    public EnvironmentGenerator EnvironmentGenerator => environmentGenerator;


    public EnvironmentalSettings environmentalSettings;


    public EnvironmentGenerator InitializeGenerator(Mesh _mesh)
    {
        switch (RenderMode)
        {
            case 1:
                environmentGenerator = new VegetationGenerator(_mesh, Material, environmentalSettings);
                break;
            case 2:
                environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, environmentalSettings);
                break;
            default:
                environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, environmentalSettings);
                break;
        }
        return environmentGenerator;
    }
}
