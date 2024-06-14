using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{

    private PlayerActionScript ActionScript;


    [SerializeField] private Transform Camera;
    [SerializeField] private float rotationSpeed;
    private Vector3 TargetRotationDirection;



    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private CinemachineVirtualCamera lockOnCamera;
    private bool lockOn;



    private CinemachineInputProvider cinemachineInputProvider;

    private void Start()
    {
        ActionScript = GetComponent<PlayerActionScript>();
        cinemachineInputProvider = freeLook.GetComponent<CinemachineInputProvider>();

        cinemachineInputProvider.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        Rotate();
    }



    private void Rotate()
    {
        if (!lockOn)
        {
            TargetRotationDirection = ActionScript.MovementVector;
        }
        else
        {
            TargetRotationDirection = lockOnCamera.transform.forward;
        }

        TargetRotationDirection.Normalize();
        TargetRotationDirection.y = 0;
        if (TargetRotationDirection == Vector3.zero) //Comparison between two Vectors works in that Case, because the Values of TargetRotation are bound to Input System.
        {
            TargetRotationDirection = transform.forward;
        }

        Quaternion turnRotation = Quaternion.LookRotation(TargetRotationDirection);
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, turnRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = newRotation;


    }

    public void AllowCameraInput(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            cinemachineInputProvider.enabled = true;
        }

        if (_context.canceled)
        {
            cinemachineInputProvider.enabled = false;
        }
    }


    public void LockOn(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            freeLook.enabled = !freeLook.enabled;
            lockOn = !lockOn;
        }

    }
}
