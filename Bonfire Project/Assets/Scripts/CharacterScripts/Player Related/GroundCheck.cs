using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Ground Check is not used in current State, because the Jump feature was taken out. However the Script remains, in case I want to readd it later.

    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] Transform CharacterPivot;


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
