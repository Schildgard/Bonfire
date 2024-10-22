using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public class FireCast : MonoBehaviour
{

    //Fire Cast collects a List of the Enemies who are in its hit Area and deals damage to them as long as they stay in it. It also checks if the Enemies are wet.
    [SerializeField] private float damagePerIntervall;

    private List<IDamageable> hitTargets;
    private List<GameObject> WetTargets;

    [SerializeField] private float damageIntervall;
    [SerializeField] private float timer;

    [SerializeField] private GameObject CloudPrefab;

    private CinemachineVirtualCamera lockOnCamera;
    private Transform target;

    AudioSource flameHitSound;

    private void Awake()
    {
        lockOnCamera = GameObject.Find("Lock On Camera").GetComponent<CinemachineVirtualCamera>();
        this.transform.parent = null;
        hitTargets = new List<IDamageable>();
        WetTargets = new List<GameObject>();
        flameHitSound = GetComponent<AudioSource>();
        if (lockOnCamera.LookAt != null)
        {
            target = lockOnCamera.LookAt.transform;
        }

    }

    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }

        if (hitTargets.Count > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {

                foreach (IDamageable target in hitTargets)
                {
                    target.GetDamage(damagePerIntervall);
                    flameHitSound.PlayOneShot(flameHitSound.clip);
                    
                }
                timer = damageIntervall;

                foreach (GameObject target in WetTargets)
                {
                    Instantiate(CloudPrefab, target.transform.position, Quaternion.identity);

                    var wetCondition = target.GetComponentInChildren<EffectCondition_Wet>();
                    wetCondition.duration = 0.01f;
                } WetTargets.Clear();
            }

        }
        else { timer = 0.1f;}
    }
    private void OnTriggerEnter(Collider _target)
    {
        if (_target.gameObject.layer == 7) { return; }
        var damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        if (damageableTarget == null) return;
        hitTargets.Add(damageableTarget);


        var wetableTarget = _target.gameObject.GetComponent<IWetable>();
        if (wetableTarget == null) return;

        var wetTarget = _target.GetComponentInChildren<EffectCondition_Wet>();
        if (wetTarget == null) return;

        WetTargets.Add(_target.gameObject);


    }

    private void OnTriggerExit(Collider _target)
    {
        var damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        hitTargets.Remove(damageableTarget);
    }
}
