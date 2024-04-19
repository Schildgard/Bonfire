using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    #region FollowPlayerVariables
    [Header("Follow Player Variables")]
    [SerializeField] private GameObject player;
    [SerializeField] private float cameraSmoothSpeed;
    [SerializeField] private Vector3 cameraVelocity;
    #endregion

    #region RotationVariables
    [Header("Rotation Variables")]
    [SerializeField] PlayerActionScript playerActionScript;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float leftAndRightLookAngle;
    [SerializeField] private float upAndDownLookAngle;

    [SerializeField] private float maxYRotation;
    [SerializeField] private float minYRotation;

    [SerializeField] private Transform cameraPivotTransform;
    #endregion

    #region CollisionVariables
    [Header("Collision Variables")]
    [SerializeField] private Transform CameraPosition;
    [SerializeField] private float cameraCollisionRadius;
    [SerializeField] private float cameraCollisionDistance;
    [SerializeField] private LayerMask WallLayerMask;

    private Vector3 CameraAndPlayerDistance;
    private Vector3 DefaultPosition;


    private Vector3 PlayerOffSet;
    [SerializeField] private float offSetValue;
    #endregion

    private void Start()
    {
        DefaultPosition = CameraPosition.position;


        CameraAndPlayerDistance = CameraPosition.position - player.transform.position;
        cameraCollisionDistance = Vector3.Magnitude(CameraAndPlayerDistance);
    }
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
        leftAndRightLookAngle += playerActionScript.CameraInput.x * rotationSpeed * Time.deltaTime;
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
        PlayerOffSet = (player.transform.position - Vector3.forward * offSetValue); // Since RayCasts ignore Colliders they are starting in, the OffSet prevents that the Camera jumps behind the wall, when the player touches it.
        CameraAndPlayerDistance = CameraPosition.position - player.transform.position; //Continous Line between Player and Camera
        hit = Physics.SphereCast(PlayerOffSet, cameraCollisionRadius, CameraAndPlayerDistance, out hitInfo, cameraCollisionDistance, WallLayerMask);

        if (hit)
        {
            //  CameraPosition.position = hitInfo.collider.ClosestPoint(player.transform.position); // blocks CameraMovement to the other Axxises
             Vector3 TargetPosition = hitInfo.collider.ClosestPoint(player.transform.position);
            CameraPosition.position = new Vector3(CameraPosition.position.x, CameraPosition.position.y, TargetPosition.z);
            Debug.Log("Camera hit something");
        }
        else
        {
            CameraPosition.localPosition = DefaultPosition;
           // Debug.Log("Camera hit nothing");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(PlayerOffSet, CameraAndPlayerDistance, Color.blue);

    }
}
