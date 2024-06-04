using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireCast : MonoBehaviour
{

    [SerializeField] private float damagePerIntervall;

    private List<IDamageable> hitTargets;
    private List<GameObject> WetTargets;

    [SerializeField] private float damageIntervall;
    [SerializeField] private float damageCounter;

    [SerializeField] private GameObject CloudPrefab;

    private void Awake()
    {
        hitTargets = new List<IDamageable>();
        WetTargets = new List<GameObject>();
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

                foreach (GameObject target in WetTargets)
                {
                    //spawnCloudVFX

                    Instantiate(CloudPrefab, target.transform.position, Quaternion.identity);
                    Debug.Log("Spawn Cloud");

                    var wetCondition = target.GetComponentInChildren<EffectCondition_Wet>();
                    wetCondition.duration = 0.01f;
                    Debug.Log("Set Duration to 0.01f");
                }
                    WetTargets.Clear();
                    Debug.Log("Cleared List");

            }

        }
        else { damageCounter = 0.3f;}
    }
    private void OnTriggerEnter(Collider _target)
    {

        var damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        if (damageableTarget == null) return;
        hitTargets.Add(damageableTarget);


        var wetableTarget = _target.gameObject.GetComponent<IWetable>();
        if (wetableTarget == null) return;
        Debug.Log(_target.name + "has IWetable");
        var wetTarget = _target.GetComponentInChildren<EffectCondition_Wet>();
        if (wetTarget == null) return;
        Debug.Log(_target.name + "has EffectCondition_wet");
        WetTargets.Add(_target.gameObject);
        Debug.Log(_target.name + "is added to List!");

    }

    private void OnTriggerExit(Collider _target)
    {
        var damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        hitTargets.Remove(damageableTarget);

      // if (WetTargets.Contains(_target.gameObject))
      // {
      //     WetTargets.Remove(_target.gameObject);
      // }
    }
}
