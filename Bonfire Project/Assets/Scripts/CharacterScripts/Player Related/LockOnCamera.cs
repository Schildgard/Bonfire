using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockOnCamera : MonoBehaviour
{
    [SerializeField] private PlayerRotation playerRotation;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject LockOnCanvas;

    private CinemachineVirtualCamera lockOnCamera;
    private CinemachineFreeLook freeLook;

    private HealthScript targetsHealthScript;

    private Animator animator;


    [SerializeField] private float scanRadius;

    private void Awake()
    {
        lockOnCamera = GetComponent<CinemachineVirtualCamera>();
        freeLook = FindAnyObjectByType<CinemachineFreeLook>();
        animator = playerTransform.GetComponent<Animator>();
    }

    private void Update()
    {
        if (lockOnCamera.LookAt == null) return;

        if (targetsHealthScript != null && !targetsHealthScript.isAlive)
        {
            ChangeTarget();
        }
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
            }
        }

        if (potentialTargets.Count > 0)
        {
            return potentialTargets;
        }
        else return null;
    }

    public Transform CheckPotentialTargets()
    {
        List<GameObject> potentialTarget = ScanForEnemiesInRange();
        if (potentialTarget == null)
        {
            return null;
        }

        List<GameObject> TargetsInFrontofPlayer = new List<GameObject>();
        foreach (GameObject enemy in potentialTarget)
        {
            Vector3 relativePosition = playerTransform.InverseTransformPoint(enemy.transform.position);

            if (relativePosition.z >= 0)
            {
                TargetsInFrontofPlayer.Add(enemy);
            }

        }
        if (TargetsInFrontofPlayer.Count <= 0)
        {
            return null;
        }

        GameObject lockOnTarget = GetNearestEnemy(TargetsInFrontofPlayer);
        Transform FocusPoint = lockOnTarget.transform.Find("FocusPoint");
        return FocusPoint;

    }
    private GameObject GetNearestEnemy(List<GameObject> _enemylist)
    {
        GameObject lockOnTarget = _enemylist[0];
        float shortestDistanceToPlayer = 10000;


        foreach (var enemy in _enemylist)
        {
            Vector3 Distance = enemy.transform.position - playerTransform.position;
            float distance = Vector3.SqrMagnitude(Distance);

            if (distance < shortestDistanceToPlayer)
            {
                lockOnTarget = enemy.gameObject;

                shortestDistanceToPlayer = distance;

            }
        }
        return lockOnTarget;
    }


    public void ToggleLockOn(InputAction.CallbackContext _context)
    {

        if (_context.started)
        {
            Transform Target = lockOnCamera.LookAt;

            if (Target != null)
            {
                DeactivateLockOn();
                return;
            }

            Target = CheckPotentialTargets();
            if (Target == null) { return; }
            ActivateLockOn(Target);
        }

    }
    public void LockOnToNextTarget(InputAction.CallbackContext _context)
    {
        if (!_context.started) return;

        float contextInput = _context.ReadValue<float>();

        List<GameObject> potentialTarget = ScanForEnemiesInRange();
        if (potentialTarget == null)
        {
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
                    //Debug.Log(enemy.name + " is right of you");
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
                    //Debug.Log(enemy.name + " is left of you");
                }
            }
        }

        if (viableTargets.Count == 0)
        {
            return;
        }

        GameObject lockOnTarget = GetNearestEnemy(viableTargets);

        Transform FocusPoint = lockOnTarget.transform.Find("FocusPoint");

        lockOnCamera.LookAt = FocusPoint;

        LockOnCanvas.transform.parent = FocusPoint;
        LockOnCanvas.transform.position = FocusPoint.position;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }


    public void ActivateLockOn(Transform _target)
    {
        lockOnCamera.LookAt = _target;
        freeLook.enabled = false;
        playerRotation.LockOn = true;
        animator.SetBool("LockOn", true);
        animator.SetTrigger("Equip");
        LockOnCanvas.SetActive(true);
        LockOnCanvas.transform.parent = _target;
        LockOnCanvas.transform.position = _target.position;

        targetsHealthScript = _target.GetComponentInParent<HealthScript>();
    }
    public void DeactivateLockOn()
    {
        LockOnCanvas.transform.parent = null;
        lockOnCamera.LookAt = null;
        freeLook.enabled = true;
        playerRotation.LockOn = false;
        animator.SetBool("LockOn", false);
        animator.SetTrigger("Unarm");
        LockOnCanvas.SetActive(false);

        targetsHealthScript = null;
    }
    public void ChangeTarget()
    {
        Transform newTarget = CheckPotentialTargets();
        if (newTarget != null)
        {
            ActivateLockOn(newTarget);
        }
        else
        {
            LockOnCanvas.transform.parent = null;
            DeactivateLockOn();
        }
    }
}
