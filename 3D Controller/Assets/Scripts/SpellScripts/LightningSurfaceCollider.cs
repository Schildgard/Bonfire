using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSurfaceCollider : MonoBehaviour
{
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
        timer -= Time.deltaTime;

        
    }
    private void OnTriggerEnter(Collider _target)
    {
        DamageTargetsOnSurface(_target.gameObject);
        ElectrifyTargetsOnSurface(_target.gameObject);
    }

    private void OnTriggerStay(Collider _target)
    {

        if(timer<= 0)
        {
            DamageTargetsOnSurface(_target.gameObject);
            ElectrifyTargetsOnSurface(_target.gameObject);
            timer = tickIntervall;
        }


    }


    private IEnumerator DespawnVFX(GameObject _object)
    {
        yield return new WaitForSeconds(despawnTimer);
        _object.SetActive(false);

    }


    private void DamageTargetsOnSurface(GameObject _target)
    {
        var hittableTarget = _target.GetComponent<IDamageable>();
        if (hittableTarget != null)
        {
            hittableTarget.GetDamage(damage);
        }
    }
    private void ElectrifyTargetsOnSurface(GameObject _target)
    {
        IElectrilizable[] electrilizableTargets = _target.GetComponentsInChildren<IElectrilizable>();
        if (electrilizableTargets != null)
        {
            foreach (var target in electrilizableTargets)
            {
                target.Electrify();
            }
        }
    }
}
