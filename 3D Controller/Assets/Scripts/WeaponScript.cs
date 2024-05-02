using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
     private StatScript WielderStats;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float knockBackValue;

    private void Start()
    {
        WielderStats = GetComponentInParent<StatScript>();
    }
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
        float damageMultiplier = (weaponDamage / 100) * (WielderStats.Strength *5);
        var hittableTarget = _target.GetComponent<IDamageable>();
        if (hittableTarget == null) return;
        hittableTarget.GetDamage(weaponDamage+damageMultiplier);
        Debug.Log($"Weapon dealt{weaponDamage + damageMultiplier} Damage");
    }
}