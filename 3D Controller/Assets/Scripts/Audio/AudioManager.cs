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

    // Start is called before the first frame update
    void Start()
    {
        
        foreach (var sound in Music)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
        }
        foreach (var sound in SFX)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.volume = sound.volume;
            sound.source.clip = sound.clip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0,1)]
    public float volume;
    public AudioSource source;
    
}
