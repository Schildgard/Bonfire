using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeshFace
{
    public MeshRenderer MeshRenderer {  get; private set; }
    public MeshFilter MeshFilter { get; private set; }


    public MeshFace(MeshRenderer meshRenderer, MeshFilter meshFilter)
    {
        MeshRenderer = meshRenderer;
        MeshFilter = meshFilter;
    }
}
