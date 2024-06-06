using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectCondition_Lightning : StatusEffect
{
    
    protected override void Awake()
    {
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
}
