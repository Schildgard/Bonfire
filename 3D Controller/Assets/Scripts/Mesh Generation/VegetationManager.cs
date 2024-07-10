using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetationManager : MonoBehaviour
{
    private VegetationGenerator vegGenerator;
    [SerializeField]private Material vegetationMaterial;
    private MeshFace vegetationMesh;
    private Mesh planeMesh;

    [SerializeField]private float threshold;


    [SerializeField] GameObject PositionIndicatorPrefab;
    private void Start()
    {
        planeMesh = GameObject.Find("Custom Plane").GetComponent<MeshFilter>().mesh;
        vegGenerator = new VegetationGenerator(planeMesh, vegetationMaterial, threshold);
        GenerateAllVegetations();

      // List<Vector3> spawnPositions = vegGenerator.CalculateSpawnPositions(planeMesh);
      // 
      // ShowSpawnPositions(spawnPositions);
    }

    private void GenerateAllVegetations()
    {
        vegetationMesh = vegGenerator.GenerateVegetationItem();

    }

    private void ShowSpawnPositions(List<Vector3> _positions)
    {
        foreach (var position in _positions)
        {
            Instantiate(PositionIndicatorPrefab, position, Quaternion.identity);
        }

    }
}
