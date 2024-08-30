using UnityEngine;


//This Script can be found on a invisible Plane which kills the player when he falls down a cliff.
public class Deathzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider _other)
    {
        if( _other.gameObject.layer == 7)
        {
            var player = _other.GetComponent<PlayerScript>();
            player.Die();
        }
    }
}
