using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] private float weaponDamage;
    [SerializeField] private float knockBackValue;
    public float KnockBackValue
    {
        get { return knockBackValue; }
    }

    public void KnockBack(Rigidbody _hitObject)
    {
        _hitObject.AddForce(transform.forward * knockBackValue, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider _target)
    {
        var hittableTarget = _target.GetComponent<IDamageable>();
        if (hittableTarget == null) return;
        hittableTarget.GetDamage(weaponDamage);
    }
}
