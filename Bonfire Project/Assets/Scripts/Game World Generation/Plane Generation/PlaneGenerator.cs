using UnityEngine;
using UnityEngine.AI;

public class PlaneGenerator
{
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


    public GameObject CreatePlaneItem()
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
        newPlane.AddComponent<NavMeshSurface>();

        DrawPlaneMesh(mesh, collider);
        return newPlane;
    }


    public void DrawPlaneMesh(Mesh _mesh, Collider _collider)
    {
        int vertexCount = resolution * resolution;
        int triangleIndexCount = 2 * (3 * ((resolution - 1) * (resolution - 1)));

        Vector3[] verticies = new Vector3[vertexCount];
        int[] triangles = new int[triangleIndexCount];
        Vector2[] uvs = new Vector2[vertexCount];

        int triangleIndex = 0;
        float falloff = 0;

        for (int y = 0, i = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++, i++)
            {

                Vector2 percent = new Vector2(x, y) / (resolution - 1);  //Calculate Vertex Position
                percent -= Vector2.one * 0.5f; //translate position to percentage of full mesh

                Vector3 planePosition = Vector3.zero;
                Vector3 transformedPosition = Vector3.zero;


                #region Falloff Calculation

                //EAST
                if (percent.x >= 0.4f && percent.y <= 0.4f && percent.y >= -0.4f)
                {
                    falloff = (0.5f - percent.x) * 10f; // Falloff Multiplier gets lower the closer you get to the Edge.
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition, falloff);
                }
                //WEST
                else if (percent.x <= -0.4f && percent.y <= 0.4f && percent.y >= -0.4f)
                {
                    falloff = (0.5f + percent.x) * 10f;
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition, falloff);
                }
                //NORTH
                 else if (percent.y >= 0.4f && percent.x <= 0.4f && percent.x >= -0.4f)
                {
                    falloff = (0.5f - percent.y) * 10f;
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition, falloff);
                }
                //SOUTH
                else if (percent.y <= -0.4f && percent.x <= 0.4f && percent.x >= -0.4f)
                {
                    falloff = (0.5f + percent.y) * 10f;
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition, falloff);
                }



                //SOUTHEAST CORNER
                else if (percent.y <= -0.4f && percent.x >= 0.4f) 
                {
                    falloff = ((0.5f + percent.y) * 10f) * ((0.5f - percent.x) * 10f);
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition, falloff);
                }
                //SOUTHWEST CORNER
                else if (percent.y <= -0.4f && percent.x <= -0.4f)
                {
                    falloff = ((0.5f + percent.y) * 10f) * ((0.5f + percent.x) * 10f);
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition, falloff);
                }
                //NORTHEAST CORNER
                else if (percent.y>= 0.4f && percent.x >= 0.4f)
                {
                    falloff = ((0.5f - percent.y) * 10f) * ((0.5f - percent.x) * 10f);
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition, falloff);
                }
                //NORTHWEST CORNER
                else if (percent.y >= 0.4f && percent.x <= -0.4f)
                {
                    falloff = ((0.5f - percent.y) * 10f) * ((0.5f + percent.x) * 10f);
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition, falloff);
                }


                #endregion



                else
                {
                    falloff = 1;
                    planePosition = ((Vector3.right * percent.x) + (Vector3.forward * percent.y));
                    transformedPosition = noiseFilter.SetNoisePosition(planePosition, falloff);

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

    public void UpdatePlaneMesh(GameObject _meshface, int _resolution)
    {
        resolution = _resolution;

        DrawPlaneMesh(_meshface.GetComponent<MeshFilter>().sharedMesh, collider);
    }
}
