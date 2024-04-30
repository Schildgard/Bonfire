using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : CharacterScript
{
    [SerializeField] GameEvent YouDied;

    [SerializeField] Transform LastBonfire;
    public override void Die()
    {
        base.Die();
        YouDied.Raise();
        transform.position = LastBonfire.position;


        //RestorePlayerStatus
        //respawn Enemies
        return;

    }
}
