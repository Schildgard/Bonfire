using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{


    [SerializeField] private float rotationSpeed;
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private CinemachineVirtualCamera lockOnCamera;
    private PlayerActionScript ActionScript;
    private Vector3 TargetRotationDirection;
    private bool lockOn;
    private CinemachineInputProvider cinemachineInputProvider;


   
    [SerializeField] private float scanRadius;

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


    public void CheckPotentialTargets(InputAction.CallbackContext _context)
    {


        List<GameObject> potentialTarget = new List<GameObject>();

        if (_context.started)
        {
            // Gather Enemies Around Player
            var targetableEnemies = Physics.OverlapSphere(transform.position, scanRadius);

            foreach (var enemy in targetableEnemies)
            {
                if (enemy.gameObject.layer == 8)
                {
                    potentialTarget.Add(enemy.gameObject);
                    Debug.Log(enemy.name + " is in LockOn Range");
                }
            }

            Debug.Log("potentialTargetList contains" + (potentialTarget.Count) + " Objects");

            //Check which Enemies are in Front of Player
            List<GameObject> TargetsInFrontofPlayer = new List<GameObject>();


            foreach (GameObject enemy in potentialTarget)
            {
              float dotProduct =  HelperFunctions.instance.GetDotProduct(transform, enemy.transform);

                if (dotProduct >= 0)
                {
                    Debug.Log("Add " + enemy.name + " to Front List");
                    TargetsInFrontofPlayer.Add(enemy);
                    Debug.Log("Adding Succesfull");
                }
                else
                {
                    Debug.Log(enemy.name + " is behind Player");
                }
            }

            //Get Distance of Enemies who are in front of Player and set nearest Enemy as Target

            GameObject lockOnTarget = TargetsInFrontofPlayer[0];
            float shortestDistanceToPlayer = 10000;


            foreach (var enemy in TargetsInFrontofPlayer)
            {
                Vector3 Distance = enemy.transform.position - transform.position;
                float distance = Vector3.SqrMagnitude (Distance);
                Debug.Log("Distance between Player and " + enemy.name + " is " + distance);
                if (distance < shortestDistanceToPlayer)
                {
                   lockOnTarget = enemy.gameObject;

                    shortestDistanceToPlayer = distance;
                    Debug.Log("shortestDistance is changed to " + distance);
                }
                else
                {
                    Debug.Log("shortestDistance remains the same");
                }
            }
            lockOnCamera.LookAt = lockOnTarget.transform;

            //Set Enemy with shortestDistance as Target of Focus Camera

        }

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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }



}
