using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    protected SkinnedMeshRenderer SkinnedMeshRenderer;

    protected Material[] OriginalMaterial;

    public float maxduration = 5;
    public float duration;

    protected virtual void Awake()
    {
        SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        OriginalMaterial = SkinnedMeshRenderer.materials;
        duration = maxduration;
    }

    protected virtual void Update()
    {
        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            SkinnedMeshRenderer.materials = OriginalMaterial;
            Destroy(this);
        }
    }
}
