using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public class FireCast : MonoBehaviour
{

    [SerializeField] private float damagePerIntervall;

    private List<IDamageable> hitTargets;
    private List<GameObject> WetTargets;

    [SerializeField] private float damageIntervall;
    [SerializeField] private float timer;

    [SerializeField] private GameObject CloudPrefab;

    private CinemachineVirtualCamera lockOnCamera;
    private Transform target;

    private void Awake()
    {
        lockOnCamera = GameObject.Find("Lock On Camera").GetComponent<CinemachineVirtualCamera>();
        this.transform.parent = null;
        hitTargets = new List<IDamageable>();
        WetTargets = new List<GameObject>();
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
        else { timer = 0.1f;} //investigte further
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
