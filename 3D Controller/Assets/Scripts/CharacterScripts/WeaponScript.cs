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

    [SerializeField] private int viableLayers;

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

        var closestPoint = _target.ClosestPoint(transform.position);
        Instantiate(onHitVFX[0], closestPoint, Quaternion.identity);
        if (_target.gameObject.layer == viableLayers)
        {

            IDamageable[] hittableTarget = _target.GetComponentsInChildren<IDamageable>();
            float damageMultiplier = (WielderStats.Strength * strengthScaling) * 50;

            if (hittableTarget == null) return;
            foreach (var hit in hittableTarget)
            {
                Debug.Log("Damage on " + hit);
                hit.GetDamage(weaponDamage + damageMultiplier);
                Debug.Log($"Weapon striked for {weaponDamage + damageMultiplier} Damage ({weaponDamage} + {damageMultiplier}");
            }
        }
    }
}
