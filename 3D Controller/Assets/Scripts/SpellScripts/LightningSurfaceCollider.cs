using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningSurfaceCollider : MonoBehaviour
{
    private List<IDamageable> DamageableTargets = new List<IDamageable>();
    private List<IElectrilizable> ElectrilizableTargets = new List<IElectrilizable>();

    [SerializeField] private float despawnTimer;

    [SerializeField] private float damage;

    [SerializeField] private float tickIntervall;
    private float timer;
    private void Start()
    {
        StartCoroutine(DespawnVFX(gameObject));
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= tickIntervall)
        {
            foreach(var target in DamageableTargets)
            {
                target.GetDamage(damage);
            }
            foreach(var target in ElectrilizableTargets)
            {
                target.Electrify();
            }
            timer = 0;
        }
    }
    private void OnTriggerEnter(Collider _target)
    {

        var hittableTarget = _target.gameObject.GetComponent<IDamageable>();
        IElectrilizable[] electrilizableTargets = _target.gameObject.GetComponentsInChildren<IElectrilizable>();
        if (hittableTarget != null)
        {

            DamageableTargets.Add(hittableTarget);
            hittableTarget.GetDamage(damage);

        }

        if (electrilizableTargets != null)
        {
            foreach (var target in electrilizableTargets)
            {
                ElectrilizableTargets.Add(target);
                target.Electrify();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        // Apply Damage/Effect over time?
    }

    private void OnTriggerExit(Collider _target)
    {
        IDamageable hittableTarget = _target.gameObject.GetComponent<IDamageable>();
        IElectrilizable electrilizableTarget = _target.gameObject.GetComponentInChildren<IElectrilizable>();
        if (DamageableTargets.Contains(hittableTarget))
        {
            DamageableTargets.Remove(hittableTarget);
        }
        if (ElectrilizableTargets.Contains(electrilizableTarget))
        {
            ElectrilizableTargets.Remove(electrilizableTarget); //Potential Error ?
        }
    }
    private IEnumerator DespawnVFX(GameObject _object)
    {
        yield return new WaitForSeconds(despawnTimer);
        _object.SetActive(false);

    }
}
