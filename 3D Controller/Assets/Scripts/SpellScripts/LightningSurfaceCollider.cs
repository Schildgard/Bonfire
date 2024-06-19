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
        IElectrilizable[] electrilizableTargets = _target.gameObject.GetComponentsInChildren<IElectrilizable>();
        if (hittableTarget != null)
        {
            hittableTarget.GetDamage(5);

        }

        if (electrilizableTargets != null)
        {
            foreach (var target in electrilizableTargets)
            {
                target.Electrify();
            }
        }

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
