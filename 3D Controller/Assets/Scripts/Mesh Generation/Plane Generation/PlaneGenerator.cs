using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator
{

    //private GameObject newPlane;
    private Material material;
    private NoiseFilter noiseFilter;
    private int resolution;

    MeshCollider collider;

    public PlaneGenerator(Material _material, NoiseFilter _noiseFilter, int _resolution)
    {
        material = _material;
        noiseFilter = _noiseFilter;
        resolution = _resolution;
    }


    public MeshFace CreatePlaneItem()
    {

        GameObject newPlane = new GameObject("Custom Plane");


        MeshRenderer meshRenderer = newPlane.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = newPlane.AddComponent<MeshFilter>();
        collider = newPlane.AddComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        mesh.name = "Plane Layer";
        meshRenderer.sharedMaterial = material;
        meshFilter.mesh = mesh;

        newPlane.isStatic = true;

        DrawPlaneMesh(mesh, collider);

        return new MeshFace(meshRenderer, meshFilter);
    }


    public void DrawPlaneMesh(Mesh _mesh, Collider _collider)
    {
        int vertexCount = resolution * resolution;
        int triangleIndexCount = 2 * (3 * ((resolution - 1) * (resolution - 1)));

        Vector3[] verticies = new Vector3[vertexCount];
        int[] triangles = new int[triangleIndexCount];
        Vector2[] uvs = new Vector2[vertexCount];

        int triangleIndex = 0;

        for (int y = 0, i = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++, i++)
            {

                Vector2 percent = new Vector2(x, y) / (resolution - 1);  //Calculate Vertex Position
                percent -= Vector2.one * 0.5f; //translate position to percentage of full mesh

                Vector3 planePosition = Vector3.zero;
                Vector3 transformedPosition = Vector3.zero;


                if (percent.x >= 0.4f|| percent.x <= -0.4f || percent.y >= 0.4f || percent.y <= -0.4f)
                {
                    planePosition = ((Vector3.right * percent.x) + (Vector3.down));
                    transformedPosition = noiseFilter.Test(planePosition);

                }
                else
                {
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));

                    //Here: Add Noise Position before setting vertex!
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition);

                }


                verticies[i] = transformedPosition;
                uvs[i] = new Vector2(x, y);

                if (y < resolution - 1 && x < resolution - 1)  //If Index is NOT on the Edge, draw a new Mesh
                {
                    triangles[triangleIndex + 0] = i;
                    triangles[triangleIndex + 1] = i + resolution;
                    triangles[triangleIndex + 2] = i + resolution + 1;

                    triangles[triangleIndex + 3] = i;
                    triangles[triangleIndex + 4] = i + resolution + 1;
                    triangles[triangleIndex + 5] = i + 1;

                    triangleIndex += 6;
                }
            }
        }

        _mesh.Clear();
        _mesh.vertices = verticies;
        _mesh.triangles = triangles;
        _mesh.uv = uvs;
        _mesh.RecalculateNormals();
        collider.sharedMesh = _mesh;


    }

    public void UpdatePlaneMesh(MeshFace _meshface, int _resolution)
    {
        resolution = _resolution;

        DrawPlaneMesh(_meshface.MeshFilter.sharedMesh, collider);
    }
}
