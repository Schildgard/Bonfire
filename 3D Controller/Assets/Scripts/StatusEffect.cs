using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    protected SkinnedMeshRenderer SkinnedMeshRenderer;

    public Material[] OriginalMaterial;
    private AudioSource AudioSource;


    public float maxduration;
    public float duration;
    public int sfxIndex;

    protected virtual void Awake()
    {
        SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        OriginalMaterial = SkinnedMeshRenderer.materials;
        duration = maxduration;
        AudioSource = gameObject.AddComponent<AudioSource>();
        
    }

    private void Start()
    {
        AudioSource.clip = AudioManager.instance.SFX[sfxIndex].clip;
        AudioSource.volume = 0.4f;
        AudioSource.Play();
    }



    protected virtual void Update()
    {
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            SkinnedMeshRenderer.materials = OriginalMaterial;
            Destroy(this);
            Destroy(AudioSource);
        }
    }
}
