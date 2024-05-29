using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LightningBoltCollider : MonoBehaviour
{


    [SerializeField] private GameObject ElectrifiedVFX;
    private ParticleSystem ParticleSystem;
    public List<ParticleCollisionEvent> CollisionEvents;

    public float particleDamage;

    [SerializeField] private Material EffectMaterial;


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
        var meshMaterials = _target.GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] oldMaterial = meshMaterials.materials;
        meshMaterials.materials = new Material[] { oldMaterial[0], EffectMaterial };
    }

}



