using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(instance);
    }

    public List<Sound> Music;

    public List<Sound> SFX;

    public List<Sound> PlayerSFX;

    // Start is called before the first frame update
    void Start()
    {

        foreach (var sound in Music)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
        foreach (var sound in SFX)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
        foreach (var sound in PlayerSFX)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }

    }
}


[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioSource source;
    [Range(0, 1)] public float volume =0.5f;
    [Range(-3f, 3f)] public float pitch = 1f;




}
