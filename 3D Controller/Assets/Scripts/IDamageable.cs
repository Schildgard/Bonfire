using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IDamageable
{
    protected void GetDamage() 
    {

    }

    protected void OnTriggerEnter(Collider collision);

}
