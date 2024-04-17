using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    #region FollowPlayerVariables
    [SerializeField] private GameObject player;
    [SerializeField] private float cameraSmoothSpeed;
    [SerializeField] private Vector3 cameraVelocity;
    #endregion

    #region RotationVariables
    [SerializeField] PlayerActionScript playerActionScript;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float leftAndRightLookAngle;
    [SerializeField] private float upAndDownLookAngle;

    [SerializeField] private float maxYRotation;
    [SerializeField] private float minYRotation;

    [SerializeField]private Transform cameraPivotTransform;
    #endregion

    [SerializeField] private Transform CameraPosition;
    [SerializeField] private float cameraCollisionRadius;
    [SerializeField] private float cameraCollisionDistance;
    [SerializeField] private LayerMask WallLayerMask;


    // Update is called once per frame
    void Update()
    {

        HandleAllCameraActions();

    }



    public void HandleAllCameraActions()
    {
        //Follow the Player
        FollowTarget();
        //Rotate around the Player
        HandleRotations();
        //Collide with Objects
        HandleCollisions();


    }

    private void FollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    private void HandleRotations() 
    {
        leftAndRightLookAngle += playerActionScript.CameraInput.x * rotationSpeed *Time.deltaTime;
        upAndDownLookAngle -= playerActionScript.CameraInput.y * rotationSpeed * Time.deltaTime;
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minYRotation, maxYRotation);


        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;

        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation); //?????????
        transform.rotation = targetRotation;

        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }
    private void HandleCollisions() 
    {
        bool hit;
        RaycastHit hitInfo;
        hit = Physics.SphereCast(CameraPosition.position, cameraCollisionRadius, Vector3.zero, out hitInfo, cameraCollisionDistance, 0);
        if (hit)
        {
            Debug.Log("hit something");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CameraPosition.position, cameraCollisionRadius);
    }
}
