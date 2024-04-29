using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour, IDestroyable
{
    public void OnHit()
    {
        Debug.Log($"{this.name} got DESTROYED");
    }
}
