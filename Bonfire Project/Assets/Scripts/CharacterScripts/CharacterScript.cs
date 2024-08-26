using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour, IDamageable
{

    protected HealthScript HealthScript;
    protected StatScript Stats;
    protected Animator Animator;
    protected Collider Collider;

    [SerializeField] protected List<Sound> CharacterSounds;


    protected virtual void Start()
    {
        HealthScript = GetComponent<HealthScript>();
        Stats = GetComponent<StatScript>();
        Animator = GetComponent<Animator>();
        Collider = GetComponentInChildren<Collider>();

        foreach (var sound in CharacterSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = 1f;
            sound.source.spread = 360f;
            sound.source.rolloffMode = AudioRolloffMode.Linear;
            sound.source.maxDistance = 50f;

        }
    }


    public virtual void GetDamage(float _damage)
    {
        if (!HealthScript.isAlive)
        { return; }

        float defMultiplier = (_damage / 100) * (Stats.Defense * 3f);
        HealthScript.CurrentHealth -= (_damage - defMultiplier);
        HealthScript.UpdateHealthBar();

        Debug.Log($"{gameObject.name} got {_damage - defMultiplier} Damage ({_damage} - {defMultiplier})");

        Animator.SetTrigger("Stagger");
        if (HealthScript.CurrentHealth <= 0)
        { Die(); }

        PlaySFXSound("Get Hit");
    }


    public virtual void Die()
    {
        if (!HealthScript.isAlive)
        { return; }
        HealthScript.isAlive = false;

        Animator.SetTrigger("Died");
        PlaySFXSound("Death");
    }

    public void PlaySFXSound(int _soundIndex)
    {
        if (!CharacterSounds[_soundIndex].source.isPlaying)
        {
            CharacterSounds[_soundIndex].source.Play();
        }
    }

    public void PlaySFXSound(string _soundname)
    {
        foreach (var sound in CharacterSounds)
        {
            if (sound.name == _soundname)
            {
                sound.source.Play();
                return;
            }
        }
        Debug.Log($"No Sound with name of {_soundname} was found in {this.gameObject.name}s SoundList. Please check for exact wording or typos");
    }
}
