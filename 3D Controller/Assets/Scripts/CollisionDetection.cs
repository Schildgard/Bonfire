using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundCheckDistance;


    // Update is called once per frame
    void Update()
    {

        CollisionCheck();

    }

    public bool CollisionCheck() 
    {

        bool hit;
        hit = Physics.Raycast(groundCheckTransform.position, Vector3.down, groundCheckDistance, layerMask: groundCheckLayerMask);
        Debug.DrawRay(groundCheckTransform.position, Vector3.down * groundCheckDistance);

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
