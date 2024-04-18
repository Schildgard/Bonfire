using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private float groundCheckDistance;


    // Update is called once per frame
    void Update()
    {

        CollisionCheck();

    }

    public bool CollisionCheck() 
    {

        bool hit;
        hit = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, layerMask: groundCheckLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance);

        if (hit)
        {
            
            Gizmos.color = Color.green;
            return true;
        }
        else
        {
            
            Gizmos.color = Color.red;
            return false;
        }
        
    }

}
