using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class HitCollisionScript : MonoBehaviour
{
    [SerializeField] private Rigidbody Rigidbody;
     //Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider _collision)
    {
        var collidingObject = _collision.GetComponent<IAttackable>();
        if (collidingObject == null) return;
        Debug.Log(gameObject.name + "was hit by " + _collision.gameObject.name);
        collidingObject.KnockBack(Rigidbody);
    }
}
