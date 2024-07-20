using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private StatScript WielderStats;

    [SerializeField] private float staminaAttackCost;
    public float StaminaAttackCost { get { return staminaAttackCost; } set { staminaAttackCost = value; } }

    [SerializeField] private float weaponDamage;

    [SerializeField] private float strengthScaling;

    private void Start()
    {
        WielderStats = GetComponentInParent<StatScript>();
    }




    private void OnTriggerEnter(Collider _target)
    {
        AudioManager.instance.PlayerSFX[0].source.Play();
        float damageMultiplier = (WielderStats.Strength * strengthScaling) * 50;
        IDamageable[] hittableTarget = _target.GetComponentsInChildren<IDamageable>();
        Debug.Log("Array contains" + hittableTarget.Length + " Objects");
        if (hittableTarget == null) return;
        foreach (var hit in hittableTarget)
        {
            hit.GetDamage(weaponDamage + damageMultiplier);
            AudioManager.instance.SFX[9].source.Play();
            Debug.Log($"Weapon dealt{weaponDamage + damageMultiplier} Damage");
        }
    }
}
