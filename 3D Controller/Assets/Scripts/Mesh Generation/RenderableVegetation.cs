using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[System.Serializable]
public class RenderableVegetation
{
    public Material Material;
    [Range(0, 1)]
    public int RenderMode;

    private EnvironmentGenerator environmentGenerator;
    public EnvironmentGenerator EnvironmentGenerator => environmentGenerator;




    public float Threshold;

    public Vector3 Offset;
    public Vector3 ScaleMultiplier;

    public bool activated;


    public EnvironmentGenerator InitializeGenerator(Mesh _mesh)
    {
        switch (RenderMode)
        {
            case 0:
                environmentGenerator = new VegetationGenerator(_mesh, Material,Threshold,  Offset, ScaleMultiplier);
                break;
            case 1:
                environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, Threshold, Offset, ScaleMultiplier);
                if(Material.enableInstancing == false) { Material.enableInstancing = true; }
                break;
            default:
                environmentGenerator = new InstancedMesh_VegetationGenerator(_mesh, Threshold, Offset, ScaleMultiplier);
                if (Material.enableInstancing == false) { Material.enableInstancing = true; }
                break;
        }
        return environmentGenerator;
    }
}
