using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    [SerializeField] private int enemyID;
    [SerializeField] private List<Sound> enemySounds;


    public int EnemyID => enemyID;

    protected override void Start()
    {
        base.Start();

        foreach(var sound in enemySounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public override void GetDamage(float _damage)
    {
        if(AudioManager.instance != null)
        {
        AudioManager.instance.SFX[10].source.pitch = 1.84f;
        }
        else { Debug.Log("Enemy tried to play Get Hit Sound Effect, but couldnt find Audio Manager in Scene"); }
        base.GetDamage(_damage);
        
    }
    public override void Die()
    {
        if (AudioManager.instance != null)
        {
        AudioManager.instance.SFX[10].source.pitch = 1.84f;
        }

        else { Debug.Log("Enemy tried to play Get Hit Sound Effect, but couldnt find Audio Manager in Scene"); }

        base.Die();

        SoulsSystem.instance.GainSouls(Stats.SoulsValue);
        Collider.enabled = false;
        //gameObject.SetActive(false);
        //drop Item
    }

    public void PlaySoundSFX(int _index)
    {
        Debug.Log($"try to play Sounds of {this.gameObject.name} Index of {_index} ");
        enemySounds[_index].source.Play();
    }

 
}
