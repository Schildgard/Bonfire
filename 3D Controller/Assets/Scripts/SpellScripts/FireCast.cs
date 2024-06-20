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
    [SerializeField] private float timer;

    [SerializeField] private GameObject CloudPrefab;

    private void Awake()
    {
        this.transform.parent = null;
        hitTargets = new List<IDamageable>();
        WetTargets = new List<GameObject>();
    }

    private void Update()
    {
        if (hitTargets.Count > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {

                foreach (IDamageable target in hitTargets)
                {
                    target.GetDamage(damagePerIntervall);
                    
                }
                AudioManager.instance.SFX[7].source.Play();
                timer = damageIntervall;

                foreach (GameObject target in WetTargets)
                {
                    Instantiate(CloudPrefab, target.transform.position, Quaternion.identity);

                    var wetCondition = target.GetComponentInChildren<EffectCondition_Wet>();
                    wetCondition.duration = 0.01f;
                }
                    WetTargets.Clear();
            }

        }
        else { timer = 0.1f;} //WTF? //Probably setted this to make cure the flame directly hits, but it doesnt work anyway
    }
    private void OnTriggerEnter(Collider _target)
    {

        var damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        if (damageableTarget == null) return;
        hitTargets.Add(damageableTarget);


        var wetableTarget = _target.gameObject.GetComponent<IWetable>();
        if (wetableTarget == null) return;

        var wetTarget = _target.GetComponentInChildren<EffectCondition_Wet>();
        if (wetTarget == null) return;
        Debug.Log(_target.name + "has EffectCondition_wet and entered the Fire Collider");
        WetTargets.Add(_target.gameObject);
        Debug.Log(_target.name + "is added to Wet List!");

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
