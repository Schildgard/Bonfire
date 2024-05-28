using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningSurfaceCollider : MonoBehaviour
{
    private void OnParticleCollision(GameObject _target)
    {
        Debug.Log("Not Hittable");
        var hittableTarget = _target.GetComponent<IDamageable>();
        if (hittableTarget == null)
        {
            
            return;
        }
        Debug.Log("Hit");
        hittableTarget.GetDamage(5);
    }
}
