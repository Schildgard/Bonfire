using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNormals : MonoBehaviour
{

    public MeshFilter meshFilter;
    private Mesh mesh;

    private void Start()
    {
       mesh = meshFilter.mesh;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Gizmos.DrawLine(mesh.vertices[i], mesh.vertices[i] + mesh.normals[i]);
        }
    }

}
