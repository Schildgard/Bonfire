using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LightningBoltCollider : MonoBehaviour
{


    //[SerializeField] private GameObject ElectrifiedSurfaceVFX;

    [SerializeField] private Material EffectMaterial;

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
        var electrizableTarget = _target.gameObject.GetComponentInChildren<IElectrilizable>();

        //Case 1
        if (damageableTarget == null && electrizableTarget == null)
        {
            return;
        }

        //case 2
        if (damageableTarget == null && electrizableTarget != null)
        {
            //ElectrifySurface(_target);
            electrizableTarget.Electrify(GetCollisionPosition(_target));  // Get Position Method

        }

        //Case 3
        if (damageableTarget != null && electrizableTarget == null)
        {
            damageableTarget.GetDamage(particleDamage);
        }

        //Case 4
        if (damageableTarget != null && electrizableTarget != null)
        {
            damageableTarget.GetDamage(particleDamage);
            //ElectrifyTarget(_target);
            electrizableTarget.Electrify(EffectMaterial);

        }
        //Might replace this with a switch

    }

    private void ElectrifyTarget(GameObject _target)
    {

        var targetRenderer = _target.GetComponentInChildren<SkinnedMeshRenderer>();
        if (targetRenderer.materials.Length <= 2) // Indicator if the the Second Material, which is the Electrify Material, already has been added or not
        {
            var Condition = targetRenderer.gameObject.AddComponent<EffectCondition_Lightning>();

            targetRenderer.materials = new Material[] { Condition.OriginalMaterial[0], EffectMaterial }; ;
        }
        else
        {
            var EnemyCondition = targetRenderer.gameObject.GetComponent<EffectCondition_Lightning>();
            EnemyCondition.duration = EnemyCondition.maxduration;
            Debug.Log(_target.name + "has already been electrified");
        }
    }

 


    private Vector3 GetCollisionPosition(GameObject _target)
    {
        int numCollisionEvents = ParticleSystem.GetCollisionEvents(_target, CollisionEvents); //when Particle collides, return 1 and add to CollisionEvents List

        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 pos = CollisionEvents[i].intersection;
            i++;
            return pos;
            
        }
        Debug.Log("Vector3.Zero was returned");
        return Vector3.zero;
        

    }

}



