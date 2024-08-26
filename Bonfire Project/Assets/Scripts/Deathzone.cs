using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
