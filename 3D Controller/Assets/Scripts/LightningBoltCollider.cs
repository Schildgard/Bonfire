using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningBoltCollider : MonoBehaviour
{


    [SerializeField] private GameObject ElectrifiedVFX;
    [SerializeField] private int threshold; //Threshold how many Effects can be active at the same Time

    private ParticleSystem ParticleSystem;

    private List<GameObject> ActiveEffects;
    public List<ParticleCollisionEvent> CollisionEvents;

    private void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        CollisionEvents = new List<ParticleCollisionEvent>();
        ActiveEffects = new List<GameObject>();
    }

    private void OnParticleCollision(GameObject _target) //Called per Particle
    {

        var electrizableTarget = _target.gameObject.GetComponent<IElectrilizable>();
        if (electrizableTarget == null) return;

        int numCollisionEvents = ParticleSystem.GetCollisionEvents(_target, CollisionEvents); //when Particle collides, return 1 and add to CollisionEvents List

        if (ActiveEffects.Count <= threshold)
        {

            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 pos = CollisionEvents[i].intersection;
                GameObject vfx = Instantiate(ElectrifiedVFX, pos, Quaternion.Euler(-90, 0, 0));
                ActiveEffects.Add(vfx);
                i++;
                StartCoroutine(DespawnVFX(vfx));
            }
        }


    }


    private IEnumerator DespawnVFX(GameObject _object)
    {
        yield return new WaitForSeconds(5);
        Destroy(_object);
        ActiveEffects.Remove(_object);

    }

}
