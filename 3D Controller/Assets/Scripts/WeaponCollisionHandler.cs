using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisionHandler : MonoBehaviour
{

    [SerializeField ]private Collider WeaponCollider;


    // Start is called before the first frame update
    void Start()
    {
        WeaponCollider.enabled = false;
    }


    public void ActivateWeaponCollider() 
    {
        WeaponCollider.enabled = true;

    }
    
    public void DeActivateWeaponCollider() 
    {
        WeaponCollider.enabled = false;

    }


}
