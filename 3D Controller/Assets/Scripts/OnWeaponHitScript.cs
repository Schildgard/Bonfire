using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWeaponHitScript : MonoBehaviour, IAttackable
{
    [SerializeField] private float knockBackValue = 10;

    public void KnockBack(Rigidbody _hitObject)
    {
        _hitObject.AddForce(transform.forward * knockBackValue, ForceMode.Impulse);
    }
}
