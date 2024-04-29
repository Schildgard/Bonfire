using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] Transform CharacterPivot;


    // Update is called once per frame
    void Update()
    {

        CollisionCheck();

    }

    public bool CollisionCheck() 
    {

        bool hit;
        hit = Physics.Raycast(CharacterPivot.position, Vector3.down, groundCheckDistance, layerMask: groundCheckLayerMask);
        

        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(CharacterPivot.position, Vector3.down * groundCheckDistance);
    }

}
