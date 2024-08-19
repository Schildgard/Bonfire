using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectCondition_Lightning : StatusEffect
{
    private Animator animator;
    private AudioClip electrifiedSound;

    protected override void Awake()
    {
        animator = GetComponentInParent<Animator>();
        animator.SetBool("Electrified", true);
        maxduration = 5;
        electrifiedSound = AudioManager.instance.EnvironmentalSFX[0].source.clip;

        base.Awake();
    }
    protected override void Start()
    {
        AudioManager.instance.EnvironmentalSFX[0].source.PlayOneShot(electrifiedSound);
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }


    private void OnDestroy()
    {
        animator.SetBool("Electrified", false);
        var wetStatus = GetComponentInChildren<EffectCondition_Wet>();
        wetStatus.Electrified = false;
    }
}
