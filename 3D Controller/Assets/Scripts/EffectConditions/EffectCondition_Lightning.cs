using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectCondition_Lightning : StatusEffect
{
    private Animator animator;

    protected override void Awake()
    {
        animator = GetComponentInParent<Animator>();
        animator.SetBool("Electrified", true);
        maxduration = 5;
        sfxIndex = 8;
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        AudioSource.clip = AudioManager.instance.SFX[sfxIndex].clip;
        AudioSource.volume = 0.4f;
        AudioSource.Play();
    }
    protected override void Update()
    {
        base.Update();
    }


    private void OnDestroy()
    {
        animator.SetBool("Electrified", false);
    }
}
