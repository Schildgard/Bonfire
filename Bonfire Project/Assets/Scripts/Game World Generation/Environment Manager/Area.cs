using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Area
{
    public GameObject areaPlane;

    public Mesh planeMesh;

    public List<EnvironmentalVegetation> renderableEnvironment;
    public List<EnvironmentalPrefabs> spawnableEnvironment;

}
