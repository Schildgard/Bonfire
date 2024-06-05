using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningSurfaceCollider : MonoBehaviour
{
    [SerializeField] private float despawnTimer;
    private void Start()
    {
        StartCoroutine(DespawnVFX(gameObject));
    }
    private void OnTriggerEnter(Collider _target)
    {

        var hittableTarget = _target.gameObject.GetComponent<IDamageable>();
        if (hittableTarget == null) return;


        hittableTarget.GetDamage(5);
    }

    private void OnTriggerStay(Collider other)
    {
        // Apply Damage/Effect over time?
    }

    private IEnumerator DespawnVFX(GameObject _object)
    {
        yield return new WaitForSeconds(despawnTimer);
        _object.SetActive(false);

    }
}
