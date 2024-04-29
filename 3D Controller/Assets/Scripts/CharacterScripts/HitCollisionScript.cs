using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class HitCollisionScript : MonoBehaviour
{
    //[SerializeField] private Rigidbody Rigidbody;
    private Animator Animator;
     //Start is called before the first frame update
    void Start()
    {
        //Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider _collision)
    {
        var collidingObject = _collision.GetComponent<IAttackable>();
        if (collidingObject == null) return;

        Animator.SetTrigger("Get Damage");
        //collidingObject.KnockBack(Rigidbody);
    }
}
