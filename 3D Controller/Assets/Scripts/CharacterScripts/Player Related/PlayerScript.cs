using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : CharacterScript
{

    [SerializeField] GameEvent YouDied;
    [SerializeField] PlayerInput PlayerInput;


    protected override void Start()
    {
        base.Start();
        if (SoulsSystem.instance == null)
        {
            Debug.Log("PlayerScript tried to Update the Souls Counter of the SoulsSystem on Start, but SoulsSystem is Null. Plase check if there is an SoulsSystem in the scene");
            return;
        }
        SoulsSystem.instance.UpdateSoulsCounter();
    }

    public override void Die()
    {
        base.Die();
        PlayerInput.enabled = false;
        StartCoroutine(WaitRespawn());
        return;

    }


    public void Respawn()
    {
        Debug.Log("Player respawned");
        HealthScript.ResetHealth();
        PlayerInput.enabled = true;
        HealthScript.isAlive = true;
    }

    IEnumerator WaitRespawn()
    {
        yield return new WaitForSeconds(3.5f);
        YouDied.Raise();

    }
}
