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

    [SerializeField] private AudioSource attackSound;

    [SerializeField] private List<GameObject> onHitVFX;

    private void Start()
    {
        WielderStats = GetComponentInParent<StatScript>();
        attackSound = GetComponent<AudioSource>();
    }




    private void OnTriggerEnter(Collider _target)
    {
        if (attackSound != null)
        {
            attackSound.Play();
        }
        else { Debug.Log("WeaponCollider tries to play Weapon Hit Sound, but could find no AUdioManager in Scene"); }

        var closestPoint = _target.ClosestPoint(transform.position);
        Instantiate(onHitVFX[0], closestPoint, Quaternion.identity);

        float damageMultiplier = (WielderStats.Strength * strengthScaling) * 50;
        IDamageable[] hittableTarget = _target.GetComponentsInChildren<IDamageable>();
        if (hittableTarget == null) return;
        foreach (var hit in hittableTarget)
        {
            hit.GetDamage(weaponDamage + damageMultiplier);
           // AudioManager.instance.SFX[9].source.Play();
            Debug.Log($"Weapon striked for {weaponDamage + damageMultiplier} Damage ({weaponDamage} + {damageMultiplier}");
        }
    }
}
