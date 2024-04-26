using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionScript : MonoBehaviour
{

    [SerializeField]private float radiusVectorX;
    [SerializeField]private float radiusVectorZ;
    [SerializeField]private float chaseSphereRadius;
    public float ChaseSphereRadius
    {
        get { return chaseSphereRadius; }
        set { chaseSphereRadius = value; }
    }
    [SerializeField]private float battleSphereRadius;
    public float BattleSphereRadius
    {
        get { return battleSphereRadius; }
        set { battleSphereRadius = value; }
    }

    [SerializeField] private LayerMask PlayerLayer;



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
        Gizmos.DrawWireSphere(transform.position, battleSphereRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.TransformVector(new Vector3(radiusVectorX, 0, radiusVectorZ)) * 10);
        Gizmos.DrawRay(transform.position, transform.TransformVector(new Vector3(-radiusVectorX, 0, radiusVectorZ)) * 10);


        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 10);
    }
}
