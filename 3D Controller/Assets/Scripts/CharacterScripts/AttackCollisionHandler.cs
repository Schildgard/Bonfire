using UnityEngine;


public class AttackCollisionHandler : MonoBehaviour
{
    //The Array serves in the Case if an Enemy can forexample Attack with both Hands.
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

    // Activate / De-Activate Weapon Collider is mainly called in Animation Events.
    public void ActivateWeaponCollider()
    {
        foreach (var weapon in weaponColliderArray) { weapon.enabled = true; }

    }

    public void DeActivateWeaponCollider()
    {
        foreach (var weapon in weaponColliderArray) { weapon.enabled = false; }
    }

    // Show Weapon Methods are by current State only relevant to Player due to Animation Retargeting Issues with Greatsword Animation Set.
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
