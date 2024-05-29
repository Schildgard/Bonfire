using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningSurfaceCollider : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(DespawnVFX(gameObject));
    }
    private void OnTriggerEnter(Collider _target)
    {
        Debug.Log("Not Hittable");
        var hittableTarget = _target.gameObject.GetComponent<IDamageable>();
        if (hittableTarget == null)
        {
            
            return;
        }
        Debug.Log("Hit");
        hittableTarget.GetDamage(5);
    }

    private IEnumerator DespawnVFX(GameObject _object)
    {
        yield return new WaitForSeconds(5);
        Destroy(_object);
        //ActiveEffects.Remove(_object);

    }
}
