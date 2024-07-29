using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructionScript : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private void OnEnable()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
