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
        if (_context.started)
        {
            List<GameObject> potentialTarget = ScanForEnemiesInRange();
            List<GameObject> TargetsInFrontofPlayer = new List<GameObject>();

            foreach (GameObject enemy in potentialTarget)
            {
                Vector3 relativePosition = transform.InverseTransformPoint(enemy.transform.position);

                if (relativePosition.z >= 0)
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
            GameObject lockOnTarget = GetNearestEnemy(TargetsInFrontofPlayer);

            lockOnCamera.LookAt = lockOnTarget.transform;
        }
    }

    public void LockOnToNextTarget(InputAction.CallbackContext _context)
    {

        if (_context.started)
        {
            float contextInput = _context.ReadValue<float>();
            Debug.Log(contextInput);

            List<GameObject> potentialTarget = new List<GameObject>();


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





            //Sort out enemies who are not in front or left of the player

            List<GameObject> viableTargets = new List<GameObject>();
            //If Input ist right stick
            if (contextInput > 0)
            {

                foreach (var enemy in potentialTarget)
                {
                    Vector3 relativePosition = transform.InverseTransformPoint(enemy.transform.position);
                    if (relativePosition.z > 0 && relativePosition.x >= 0)
                    {
                        viableTargets.Add(enemy.gameObject);
                        Debug.Log(enemy.name + " is right of you");
                    }
                }
            }

            //If Input is left stick
            if (contextInput < 0)
            {

                foreach (var enemy in potentialTarget)
                {
                    Vector3 relativePosition = transform.InverseTransformPoint(enemy.transform.position);
                    if (relativePosition.z > 0 && relativePosition.x <= 0)
                    {
                        viableTargets.Add(enemy.gameObject);
                        Debug.Log(enemy.name + " is left of you");
                    }
                }
            }



            // Get Distance of closest Target of chosen Direction

            GameObject lockOnTarget = viableTargets[0];
            float shortestDistanceToPlayer = 10000;

            foreach (var enemy in viableTargets)
            {
                Vector3 Distance = enemy.transform.position - transform.position;
                float distance = Vector3.SqrMagnitude(Distance);
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




        }

    }


    private List<GameObject> ScanForEnemiesInRange()
    {
        List<GameObject> potentialTargets = new List<GameObject>();
        // Gather Enemies Around Player
        var targetableEnemies = Physics.OverlapSphere(transform.position, scanRadius);

        foreach (var enemy in targetableEnemies)
        {
            if (enemy.gameObject.layer == 8)
            {
                potentialTargets.Add(enemy.gameObject);
                Debug.Log(enemy.name + " is in LockOn Range");
            }
        }

        Debug.Log("potentialTargetList contains" + (potentialTargets.Count) + " Objects");

        return potentialTargets;
    }

    private GameObject GetNearestEnemy(List<GameObject> _enemylist)
    {
        GameObject lockOnTarget = _enemylist[0];
        float shortestDistanceToPlayer = 10000;


        foreach (var enemy in _enemylist)
        {
            Vector3 Distance = enemy.transform.position - transform.position;
            float distance = Vector3.SqrMagnitude(Distance);
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
        return lockOnTarget;
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
