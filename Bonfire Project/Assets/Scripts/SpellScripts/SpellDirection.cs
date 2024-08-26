using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDirection : MonoBehaviour
{

    private Transform CameraTransform;
    [SerializeField] private Transform PlayerTransform;
    // Start is called before the first frame update

    private void Start()
    {
        CameraTransform = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(CameraTransform.localEulerAngles.x, PlayerTransform.localEulerAngles.y, 0);
    }
}
