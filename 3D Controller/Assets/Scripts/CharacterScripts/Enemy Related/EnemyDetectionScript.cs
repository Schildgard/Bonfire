using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionScript : MonoBehaviour
{

    [SerializeField] private float radiusVectorX;
    [SerializeField] private float radiusVectorZ;

    [SerializeField] private float viewRange;

    [SerializeField] private float chaseSphereRadius;
    [SerializeField] private float strafingSphereRadius;
    [SerializeField] private float attackSphereRadius;
    [SerializeField] private float alarmRadius;

    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField]private bool detected;

    public bool Detected { get { return detected; } set { detected = value; } }

    public LayerMask EnemyLayer => enemyLayer;

    public float ViewRange
    {
        get { return viewRange; }
    }
    public float ChaseSphereRadius
    {
        get { return chaseSphereRadius; }
        set { chaseSphereRadius = value; }
    }
    public float StrafingSphereRadius
    {
        get { return strafingSphereRadius; }
        set { strafingSphereRadius = value; }
    }
    public float AttackSphereRadius
    {
        get { return attackSphereRadius; }
        set { attackSphereRadius = value; }
    }

    public float AlarmRadius => alarmRadius;


    public bool CheckRange(float _checkRadius)
    {
        Collider[] col;
        col = Physics.OverlapSphere(transform.position, _checkRadius, layerMask: PlayerLayer);

        if (col.Length > 0)
        {
            return true;
        }
        return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseSphereRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, strafingSphereRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackSphereRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, alarmRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.TransformVector(new Vector3(radiusVectorX, 0, radiusVectorZ)) * 10);
        Gizmos.DrawRay(transform.position, transform.TransformVector(new Vector3(-radiusVectorX, 0, radiusVectorZ)) * 10);


        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * viewRange);


    }
}
