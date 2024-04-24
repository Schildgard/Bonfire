using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitCollision : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private Collider BodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        BodyCollider = GetComponent<Collider>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.layer == 10)
        {
            ContactPoint[] contactPoints = new ContactPoint[_other.contactCount];
            _other.GetContacts(contactPoints);

            foreach (var contactPointE in contactPoints)
            {
                Debug.Log(contactPointE);
            }
        }

    }


}

