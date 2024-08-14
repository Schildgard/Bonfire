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
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
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
