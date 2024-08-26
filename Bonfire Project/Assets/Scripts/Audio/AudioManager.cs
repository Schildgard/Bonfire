using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public List<Sound> EnvironmentalSFX;
    public List<Sound> UISFX;
    // private int currentMusicIndex = 0;


    void Start()
    {

        foreach (var sound in Music)
        {
            InitializeAudioSources(sound);
            sound.source.loop = true;
        }
        foreach (var sound in EnvironmentalSFX)
        {
            InitializeAudioSources(sound);
            sound.source.loop = true;
        }
        foreach (var sound in UISFX)
        {
            InitializeAudioSources(sound);
        }

        if (GetCurrentSceneIndex() == 0)
        {
            Music[0].source.Play();
        }
    }


    private void InitializeAudioSources(Sound _sound)
    {
        _sound.source = gameObject.AddComponent<AudioSource>();
        _sound.source.clip = _sound.clip;
        _sound.source.volume = _sound.volume;
        _sound.source.pitch = _sound.pitch;

    }

    //public void PlayAudioSound(Sound _sound)
    //{
    //    _sound.source.Play();
    //}


    public void PlayEnvironmentalSFX(int _sfxIndex)
    {
        EnvironmentalSFX[_sfxIndex].source.Play();
    }

    public void StopPlayEnvironmentalSFX(int _sfxIndex)
    {
        EnvironmentalSFX[_sfxIndex].source.Stop();
    }

    public void StartMusic(int _musicIndex)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInTrack(_musicIndex));
    }
    public void StopMusic(int _musicIndex)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutTrack(_musicIndex));
    }

    //  public void ChangeBackGroundMusic(int _musicListIndex)
    //  {
    //      StopAllCoroutines();
    //      StartCoroutine(FadeBetweenTracks(_musicListIndex));
    //  }

    // public void ChangeBackGroundMusic()
    // {
    //     StopAllCoroutines();
    //     StartCoroutine(FadeBetweenTracks(currentMusicIndex));
    // }

    IEnumerator FadeInTrack(int _musicListIndex)
    {
        float fadeTimer = 1.25f;
        float elapseTime = 0;
        Music[_musicListIndex].source.Play();

        while (elapseTime < fadeTimer)
        {
            Music[_musicListIndex].source.volume = Mathf.Lerp(0, 0.5f, elapseTime / fadeTimer);
            elapseTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator FadeOutTrack(int _musicListIndex)
    {
        float fadeTimer = 1.25f;
        float elapseTime = 0;

        while (elapseTime < fadeTimer)
        {
            Music[_musicListIndex].source.volume = Mathf.Lerp(0.5f, 0f, elapseTime / fadeTimer);
            elapseTime += Time.deltaTime;
            yield return null;
        }
        Music[_musicListIndex].source.Stop();
    }



    private int GetCurrentSceneIndex()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;

        return sceneIndex;
    }
}


[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioSource source;
    [Range(0, 1)] public float volume = 0.5f;
    [Range(-3f, 3f)] public float pitch = 1f;




}
