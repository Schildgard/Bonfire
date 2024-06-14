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
        SoulsSystem.instance.UpdateSoulsCounter();
    }

    public override void Die()
    {
        base.Die();
        PlayerInput.enabled = false;
        StartCoroutine(WaitRespawn());
        return;

    }


    public override void Respawn()
    {
        base.Respawn();
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
