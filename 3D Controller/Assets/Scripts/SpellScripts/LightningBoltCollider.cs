using UnityEngine;

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
        IElectrilizable[] electrizableTargets = _target.gameObject.GetComponentsInChildren<IElectrilizable>(false);

        if (damageableTarget != null && electrizableTargets.Length > 0)
        {

            damageableTarget.GetDamage(particleDamage * 1.5f);
            SearchAndTriggerElectrifyComponents(_target, electrizableTargets);

           
            return;
        }
        if (damageableTarget != null)
        {
            damageableTarget.GetDamage(particleDamage);
        }

        if (electrizableTargets.Length > 0)
        {
            SearchAndTriggerElectrifyComponents(_target, electrizableTargets);

            return;
        }
    }




    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitSphereRadius);

        Gizmos.DrawRay(transform.position, transform.forward * maxHitDistance);
    }


    private void SearchAndTriggerElectrifyComponents(GameObject _target, IElectrilizable[] _electrilizables)
    {
        foreach (var item in _electrilizables)
        {
            if (_target.gameObject.layer == 4)
            {
                item.Electrify(hitPosition);
            }
            else
            {
                item.Electrify();
            }
        }
    }






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



