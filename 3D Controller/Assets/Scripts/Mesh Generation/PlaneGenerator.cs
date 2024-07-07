using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator
{
    private Material material;
    private NoiseFilter noiseFilter;
    private int resolution;
    private int size;

    public PlaneGenerator(Material _material, NoiseFilter _noiseFilter, int _resolution, int _size)
    {
        material = _material;
        noiseFilter = _noiseFilter;
        resolution = _resolution;
        size = _size;
    }


    public MeshFace CreatePlaneItem()
    {
        GameObject newPlane = new GameObject("Custom Plane");

        MeshRenderer meshRenderer = newPlane.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = newPlane.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        mesh.name = "Plane Layer";
        meshRenderer.sharedMaterial = material;
        meshFilter.mesh = mesh;


        DrawPlaneMesh(mesh);

        return new MeshFace(meshRenderer, meshFilter);
    }
    public void DrawPlaneMesh(Mesh _mesh)
    {
        int vertexCount = resolution * resolution;
        int triangleIndexCount = 2 * (3 * ((resolution - 1) * (resolution - 1)));

        Vector3[] verticies = new Vector3[vertexCount];
        int[] triangles = new int[triangleIndexCount];

        int triangleIndex = 0;

        for (int y = 0, i = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++, i++)
            {
               
                Vector2 percent = new Vector2(x, y) / (resolution - 1);  //Calculate Vertex Position
                percent -= Vector2.one * 0.5f; //translate position to percentage of full mesh

                Vector3 planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y)) * size;


                //Here: Add Noise Position before setting vertex!
                Vector3 transformedPosition = noiseFilter.SetNoisePosition(planePosition);

                verticies[i] = transformedPosition;

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
        _mesh.RecalculateNormals();

    }

    public void UpdatePlaneMesh(MeshFace _meshface, int _resolution, int _size)
    {
        resolution = _resolution;
        size = _size;

        DrawPlaneMesh(_meshface.MeshFilter.mesh);
    }
}
