using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LightningBoltCollider : MonoBehaviour
{

    [SerializeField] private float hitSphereRadius;
    [SerializeField] private float particleDamage;
    [SerializeField] private float maxHitDistance;
    private Vector3 hitPosition;

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
            Debug.Log(hitInfo.transform.gameObject.name + " was hit");
            return hitInfo.transform.gameObject;
        };
        return null;

    }



    private void HitWithThisSpell()
    {
        GameObject _target = CastHitSphere();
        if (_target == null) return;



        IDamageable damageableTarget = _target.gameObject.GetComponent<IDamageable>();
        IElectrilizable[] electrizableTargets = _target.gameObject.GetComponentsInChildren<IElectrilizable>();


        if (damageableTarget != null)
        {
            damageableTarget.GetDamage(particleDamage);
        }

        if (electrizableTargets != null)
        {
            foreach (var target in electrizableTargets)
            {
                if (_target.gameObject.layer == 4)
                {
                    target.Electrify(hitPosition);
                }
                else
                target.Electrify();
            }
            return;
        }
        Debug.Log(_target.name + "  is neither damageable nor electrilizable");

    }




    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitSphereRadius);

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



