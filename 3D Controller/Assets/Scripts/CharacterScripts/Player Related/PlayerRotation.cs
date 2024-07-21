using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private CinemachineVirtualCamera lockOnCamera;
    private PlayerActionScript ActionScript;
    private Vector3 TargetRotationDirection;

    [SerializeField]private bool blockRotation;


    private bool lockOn;
    public bool LockOn { get { return lockOn; } set { lockOn = value; } }


    private void Start()
    {
        ActionScript = GetComponent<PlayerActionScript>();
    }


    // Update is called once per frame
    void FixedUpdate()
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

        if (!blockRotation)
        {
            transform.rotation = newRotation;
        }
    }



    public void BlockRotation()
    {
        Debug.Log("Rotation is blocked");
        blockRotation = true;
    }

    public void UnblockRotation()
    {
        blockRotation = false;
    }
}
