using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDirection : MonoBehaviour
{

    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Transform PlayerTransform;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(CameraTransform.localEulerAngles.x, PlayerTransform.localEulerAngles.y, 0);
    }
}
