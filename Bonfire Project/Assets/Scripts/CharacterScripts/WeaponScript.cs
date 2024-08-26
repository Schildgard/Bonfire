using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private StatScript wielderStats;
    [SerializeField] private float staminaAttackCost;

    public float StaminaAttackCost { get { return staminaAttackCost; } set { staminaAttackCost = value; } }

    [SerializeField] private float weaponDamage;
    [SerializeField] private float strengthScaling;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private List<GameObject> onHitVFX;
    [SerializeField] private int viableLayers;
    [SerializeField] private List<AudioClip> hitSounds;


    private void Start()
    {
        wielderStats = GetComponentInParent<StatScript>();
        attackSound = GetComponent<AudioSource>();
    }




    private void OnTriggerEnter(Collider _target)
    {

        if (_target.gameObject.layer == viableLayers)
        {
            IDamageable[] hittableTarget = _target.GetComponentsInChildren<IDamageable>();
            float damageMultiplier = (wielderStats.Strength * strengthScaling) * 50;

            if (hittableTarget == null) return;
            foreach (var hit in hittableTarget)
            {
                hit.GetDamage(weaponDamage + damageMultiplier);
                attackSound.PlayOneShot(hitSounds[1]);
                Debug.Log($"Weapon striked for {weaponDamage + damageMultiplier} Damage ({weaponDamage} + {damageMultiplier}");
            }
        }
        attackSound.PlayOneShot(hitSounds[0]);
        var closestPoint = _target.ClosestPoint(transform.position);
        Instantiate(onHitVFX[0], closestPoint, Quaternion.identity);


    }
}
