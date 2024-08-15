using UnityEngine;


public class AttackCollisionHandler : MonoBehaviour
{
    [SerializeField] private Collider[] WeaponColliderArray;

    //TODO: This Values are only relevant for the PlayerWeapon and should not be handled in a Script that is used by other entities.
    private bool IsWeaponOnBack;
    [SerializeField] private GameObject WeaponOnBack;
    [SerializeField] private GameObject WeaponInHand;


    void Start()
    {
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
        foreach (var weapon in WeaponColliderArray) { weapon.enabled = true; }

    }

    public void DeActivateWeaponCollider()
    {
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
