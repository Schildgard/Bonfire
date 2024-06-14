using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunctions : MonoBehaviour
{

    public static HelperFunctions instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(instance);
    }
    public float GetDotProduct(Transform _currentPosition, Transform _targetPosition)
    {
        Vector3 ViewDirection = _currentPosition.forward;

        Vector3 TargetDirection = (_targetPosition.position - _currentPosition.position);



        float magnitudeViewDirection = Vector3.Magnitude(ViewDirection);
        float magnitudeDistanceDirection = Vector3.Magnitude(TargetDirection);

        float dotProduct = (ViewDirection.x * TargetDirection.x) + (ViewDirection.y * TargetDirection.y) + (ViewDirection.z * TargetDirection.z);

        float degrees = dotProduct / (magnitudeViewDirection * magnitudeDistanceDirection);


        return 0;



    }
}
