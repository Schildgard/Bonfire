using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningBoltCollider : MonoBehaviour
{


    [SerializeField] private GameObject ElectrifiedVFX;
    private ParticleSystem ParticleSystem;
    public List<ParticleCollisionEvent> CollisionEvents;



    private void Start()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        CollisionEvents = new List<ParticleCollisionEvent>();
    }


    private void OnParticleCollision(GameObject _target) //Called per Particle
    {

        var electrizableTarget = _target.gameObject.GetComponent<IElectrilizable>();
        if (electrizableTarget == null) return;

        int numCollisionEvents = ParticleSystem.GetCollisionEvents(_target, CollisionEvents); //when Particle collides, return 1 and add to CollisionEvents List


        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 pos = CollisionEvents[i].intersection;
            GameObject vfx = Instantiate(ElectrifiedVFX, pos, Quaternion.Euler(-90, 0, 0));
            i++;
        }
    }


}



