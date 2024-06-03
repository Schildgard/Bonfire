using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireCast : MonoBehaviour
{

    [SerializeField] private float damagePerIntervall;

    private List<IDamageable> hitTargets;

    [SerializeField] private float damageIntervall;
    [SerializeField] private float damageCounter;

    private void Awake()
    {
        hitTargets = new List<IDamageable>();
    }

    private void Update()
    {
        if (hitTargets.Count > 0)
        {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0)
            {

                foreach (IDamageable target in hitTargets)
                {
                    target.GetDamage(damagePerIntervall);
                    
                }
                AudioManager.instance.SFX[7].source.Play();
                damageCounter = damageIntervall;
            }

        }
        else { damageCounter = 0.3f;}
    }
    private void OnTriggerEnter(Collider _target)
    {
        var damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        if (damageableTarget == null) return;
        hitTargets.Add(damageableTarget);

    }

    private void OnTriggerExit(Collider _target)
    {
        var damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        hitTargets.Remove(damageableTarget);
    }
}
