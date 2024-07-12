using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnvironmentGenerator
{
    protected Noise noise;
    protected Vector3[] planePositions;
    protected List<List<Matrix4x4>> matrices;

    protected EnvironmentalSettings environmentalSettings;
    public EnvironmentalSettings EnvironmentalSettings => environmentalSettings;
    public List<List<Matrix4x4>> Matrices => matrices;

    public virtual Mesh CreateEnvironmentalMesh()
    {
        //PlaceHolder
        return null;
    }

}
