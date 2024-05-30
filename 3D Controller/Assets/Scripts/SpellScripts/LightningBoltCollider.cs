using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LightningBoltCollider : MonoBehaviour
{


    [SerializeField] private GameObject ElectrifiedVFX;
    [SerializeField] private Material EffectMaterial;
    [SerializeField] private GameObject EffectCondition;

    private ParticleSystem ParticleSystem;
    public List<ParticleCollisionEvent> CollisionEvents;

    public float particleDamage;



    private void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        CollisionEvents = new List<ParticleCollisionEvent>();
    }


    private void OnParticleCollision(GameObject _target) //Called per Particle
    {

        var damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        if (damageableTarget != null)
        {
            damageableTarget.GetDamage(particleDamage);
            ElectrifyTarget(_target);
        }



        var electrizableTarget = _target.gameObject.GetComponent<IElectrilizable>();
        if (electrizableTarget == null) return;

        if (damageableTarget != null)
        {

        }


        int numCollisionEvents = ParticleSystem.GetCollisionEvents(_target, CollisionEvents); //when Particle collides, return 1 and add to CollisionEvents List


        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 pos = CollisionEvents[i].intersection;
            GameObject vfx = Instantiate(ElectrifiedVFX, pos, Quaternion.Euler(-90, 0, 0));
            i++;
        }
    }

    private void ElectrifyTarget(GameObject _target)
    {
        
        var targetRenderer = _target.GetComponentInChildren<SkinnedMeshRenderer>();
        if (targetRenderer.materials.Length < 2) // Indicator if the the Second Material, which is the Electrify Material, already has been added or not
        {
            Material[] oldMaterial = targetRenderer.materials;
            targetRenderer.gameObject.AddComponent<EffectCondition_Wet>();
            
            targetRenderer.materials = new Material[] { oldMaterial[0], EffectMaterial };;
        }
        else
        {
            var EnemyCondition = targetRenderer.gameObject.GetComponent<EffectCondition_Wet>();
            EnemyCondition.duration = EnemyCondition.maxduration;
            Debug.Log(_target.name + "has already been electrified");
        }
    }

}



