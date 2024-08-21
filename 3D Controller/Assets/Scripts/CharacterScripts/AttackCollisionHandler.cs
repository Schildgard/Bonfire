using UnityEngine;


public class AttackCollisionHandler : MonoBehaviour
{
    [SerializeField] private Collider[] weaponColliderArray;

    //TODO: This Values are only relevant for the PlayerWeapon and should not be handled in a Script that is used by other entities.
    private bool isWeaponOnBack;
    [SerializeField] private GameObject weaponOnBack;
    [SerializeField] private GameObject weaponInHand;


    void Start()
    {
        foreach (var weapon in weaponColliderArray)
        {
            weapon.enabled = false;
            if (this.gameObject.tag == "Player")
            {

                if (weaponOnBack.activeSelf && weaponInHand.activeSelf)
                {
                    weaponInHand.SetActive(false);
                    isWeaponOnBack = true;
                }
            }
        }
    }


    public void ActivateWeaponCollider()
    {
        foreach (var weapon in weaponColliderArray) { weapon.enabled = true; }

    }

    public void DeActivateWeaponCollider()
    {
        foreach (var weapon in weaponColliderArray) { weapon.enabled = false; }
    }

    public void ShowWeaponInHand()
    {
        if (weaponOnBack)
        {
            weaponOnBack.SetActive(false);
            weaponInHand.SetActive(true);
            isWeaponOnBack = false;
        }

    }

    public void ShowWeaponOnBack()
    {
        if (!isWeaponOnBack)
        {
            weaponInHand.SetActive(false);
            weaponOnBack.SetActive(true);
            isWeaponOnBack = true;
        }
    }

}
