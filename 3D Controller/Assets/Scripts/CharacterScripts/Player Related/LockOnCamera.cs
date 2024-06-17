using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockOnCamera : MonoBehaviour
{
    [SerializeField] private PlayerRotation playerRotation;
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private Transform playerTransform;
    private CinemachineVirtualCamera lockOnCamera;



    [SerializeField] private float scanRadius;

    private void Awake()
    {
        lockOnCamera = GetComponent<CinemachineVirtualCamera>();
    }



    public void CheckPotentialTargets(InputAction.CallbackContext _context)
    {
        if (!_context.started) return;

        List<GameObject> potentialTarget = ScanForEnemiesInRange();
        if (potentialTarget == null)
        {
            Debug.Log("No Enemies in Range");
            return;
        }

        List<GameObject> TargetsInFrontofPlayer = new List<GameObject>();
        foreach (GameObject enemy in potentialTarget)
        {
            Vector3 relativePosition = playerTransform.InverseTransformPoint(enemy.transform.position);

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
        if (TargetsInFrontofPlayer.Count <= 0)
        {
            Debug.Log("No Enemies in Front of Player, probably Behind");
            return;
        }

        GameObject lockOnTarget = GetNearestEnemy(TargetsInFrontofPlayer);

        lockOnCamera.LookAt = lockOnTarget.transform;

    }

    public void LockOnToNextTarget(InputAction.CallbackContext _context)
    {
        if (!_context.started) return;

        float contextInput = _context.ReadValue<float>();

        List<GameObject> potentialTarget = ScanForEnemiesInRange();
        if (potentialTarget == null)
        {
            Debug.Log("No Enemies in Range");
            return;
        }

        List<GameObject> viableTargets = new List<GameObject>();
        //If Input is right stick
        if (contextInput > 0)
        {

            foreach (var enemy in potentialTarget)
            {
                Vector3 relativePosition = playerTransform.InverseTransformPoint(enemy.transform.position);
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
                Vector3 relativePosition = playerTransform.InverseTransformPoint(enemy.transform.position);
                if (relativePosition.z > 0 && relativePosition.x <= 0)
                {
                    viableTargets.Add(enemy.gameObject);
                    Debug.Log(enemy.name + " is left of you");
                }
            }
        }

        if (viableTargets.Count == 0)
        {
            Debug.Log("No Enemy in chosen Direction");
            return;
        }

        GameObject lockOnTarget = GetNearestEnemy(viableTargets);

        lockOnCamera.LookAt = lockOnTarget.transform;


    }


    private List<GameObject> ScanForEnemiesInRange()
    {
        List<GameObject> potentialTargets = new List<GameObject>();
        // Gather Enemies Around Player
        var targetableEnemies = Physics.OverlapSphere(playerTransform.position, scanRadius);

        foreach (var enemy in targetableEnemies)
        {
            if (enemy.gameObject.layer == 8)
            {
                potentialTargets.Add(enemy.gameObject);
                Debug.Log(enemy.name + " is in LockOn Range");
            }
        }

        Debug.Log("potentialTargetList contains" + (potentialTargets.Count) + " Objects");

        if (potentialTargets.Count > 0)
        {
            return potentialTargets;
        }
        else return null;
    }

    private GameObject GetNearestEnemy(List<GameObject> _enemylist)
    {
        GameObject lockOnTarget = _enemylist[0];
        float shortestDistanceToPlayer = 10000;


        foreach (var enemy in _enemylist)
        {
            Vector3 Distance = enemy.transform.position - playerTransform.position;
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


    public void LockOn(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            freeLook.enabled = !freeLook.enabled;
            playerRotation.LockOn = !playerRotation.LockOn;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
