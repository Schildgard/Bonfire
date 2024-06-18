using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private StatScript WielderStats;

    [SerializeField]private float staminaAttackCost;
    public float StaminaAttackCost { get { return staminaAttackCost; } set { staminaAttackCost = value; } }

    [SerializeField] private float weaponDamage;

    private float strengthScaling;

    private void Start()
    {
        WielderStats = GetComponentInParent<StatScript>();
    }




    private void OnTriggerEnter(Collider _target)
    {
       // float damageMultiplier = (weaponDamage / 100) * (WielderStats.Strength * 5);
        float damageMultiplier = (WielderStats.Strength * strengthScaling) * 50;
        var hittableTarget = _target.GetComponent<IDamageable>();
        if (hittableTarget == null) return;
        hittableTarget.GetDamage(weaponDamage + damageMultiplier);
        AudioManager.instance.SFX[9].source.Play();
        Debug.Log($"Weapon dealt{weaponDamage + damageMultiplier} Damage");
    }
}
