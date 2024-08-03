using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackCollisionHandler : MonoBehaviour
{
   // [SerializeField] private Collider WeaponCollider;

    [SerializeField] private Collider[] WeaponColliderArray;

    private bool IsWeaponOnBack;

    [SerializeField] private GameObject WeaponOnBack;

    [SerializeField] private GameObject WeaponInHand;


    // Start is called before the first frame update
    void Start()
    {
   //    WeaponCollider.enabled = false;
   //    if (this.gameObject.tag == "Player")
   //    {
   //
   //        if (WeaponOnBack.activeSelf && WeaponInHand.activeSelf)
   //        {
   //            WeaponInHand.SetActive(false);
   //            IsWeaponOnBack = true;
   //        }
   //    }


        foreach (var weapon in WeaponColliderArray)
        {
            weapon.enabled = false;
            if (this.gameObject.tag == "Player")
            {

                if (WeaponOnBack.activeSelf && WeaponInHand.activeSelf)
                {
                    WeaponInHand.SetActive(false);
                    IsWeaponOnBack = true;
                }
            }
        }
    }


    public void ActivateWeaponCollider()
    {
      //  WeaponCollider.enabled = true;

        foreach (var weapon in WeaponColliderArray) { weapon.enabled = true; }

    }

    public void DeActivateWeaponCollider()
    {
    //    WeaponCollider.enabled = false;
        foreach (var weapon in WeaponColliderArray) { weapon.enabled = false; }

    }

    public void ShowWeaponInHand()
    {
        if (WeaponOnBack)
        {
            WeaponOnBack.SetActive(false);
            WeaponInHand.SetActive(true);
            IsWeaponOnBack = false;
        }

    }

    public void ShowWeaponOnBack()
    {
        if (!IsWeaponOnBack)
        {
            WeaponInHand.SetActive(false);
            WeaponOnBack.SetActive(true);
            IsWeaponOnBack = true;
        }
    }

}
