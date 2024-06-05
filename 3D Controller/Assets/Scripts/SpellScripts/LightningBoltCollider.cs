using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LightningBoltCollider : MonoBehaviour
{

    [SerializeField] private Material EffectMaterial;
    [SerializeField] private float hitSphereRadius;
    [SerializeField] private float particleDamage;
    [SerializeField] private float maxHitDistance;

   // private ParticleSystem ParticleSystem;
   // public List<ParticleCollisionEvent> CollisionEvents;

    private Vector3 hitPosition;


    private void Start()
    {
      //  ParticleSystem = GetComponent<ParticleSystem>();
      //  CollisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnEnable()
    {
        HitWithThisSpell();
    }




    private GameObject CastHitSphere()
    {

        bool hit;
        RaycastHit hitInfo;
        hit = Physics.SphereCast(transform.position, hitSphereRadius, transform.forward, out hitInfo, maxHitDistance);

        if (hit)
        {
            hitPosition = hitInfo.point;
            return hitInfo.transform.gameObject;
        }

        Debug.Log("Nothing was Hit");
        return null;

    }



    private void HitWithThisSpell()
    {
        GameObject _target = CastHitSphere();
        if (_target == null) return;



        var damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        var electrizableTarget = _target.gameObject.GetComponentInChildren<IElectrilizable>();

        //Case 1
        if (damageableTarget == null && electrizableTarget == null)
        {
                Debug.Log(_target.name + " was hitted but is wether damageable nor electrilizable");
                return;
        }

        //case 2
        if (damageableTarget == null && electrizableTarget != null)
        {
            electrizableTarget.Electrify(hitPosition);
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




    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,hitSphereRadius);

        Gizmos.DrawRay(transform.position, transform.forward * maxHitDistance);
    }






  //  private void ElectrifyTarget(GameObject _target)
  //  {
  //
  //      var targetRenderer = _target.GetComponentInChildren<SkinnedMeshRenderer>();
  //      if (targetRenderer.materials.Length <= 2) // Indicator if the the Second Material, which is the Electrify Material, already has been added or not
  //      {
  //          var Condition = targetRenderer.gameObject.AddComponent<EffectCondition_Lightning>();
  //
  //          targetRenderer.materials = new Material[] { Condition.OriginalMaterial[0], EffectMaterial }; ;
  //      }
  //      else
  //      {
  //          var EnemyCondition = targetRenderer.gameObject.GetComponent<EffectCondition_Lightning>();
  //          EnemyCondition.duration = EnemyCondition.maxduration;
  //          Debug.Log(_target.name + "has already been electrified");
  //      }
  //  }




  //  private Vector3 GetCollisionPosition(GameObject _target)
  //  {
  //      int numCollisionEvents = ParticleSystem.GetCollisionEvents(_target, CollisionEvents); //when Particle collides, return 1 and add to CollisionEvents List
  //
  //      for (int i = 0; i < numCollisionEvents; i++)
  //      {
  //          Vector3 pos = CollisionEvents[i].intersection;
  //          i++;
  //          return pos;
  //
  //      }
  //      Debug.Log("Vector3.Zero was returned");
  //      return Vector3.zero;
  //
  //
  //  }

}



