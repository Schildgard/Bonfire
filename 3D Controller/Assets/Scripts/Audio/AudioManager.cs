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
            InitializeAudioSources(sound);
            sound.source.loop = true;
        }
        foreach (var sound in SFX)
        {
            InitializeAudioSources(sound);
        }
        foreach (var sound in PlayerSFX)
        {
            InitializeAudioSources(sound);
        }

        Music[0].source.Play();

    }

    private void InitializeAudioSources(Sound _sound)
    {
        _sound.source = gameObject.AddComponent<AudioSource>();
        _sound.source.clip = _sound.clip;
        _sound.source.volume = _sound.volume;
        _sound.source.pitch = _sound.pitch;
    }

    public void PlayAudioSound(Sound _sound)
    {
        _sound.source.Play();
    }
}


[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [HideInInspector] public AudioSource source;
    [Range(0, 1)] public float volume =0.5f;
    [Range(-3f, 3f)] public float pitch = 1f;




}
