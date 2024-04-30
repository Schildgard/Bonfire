using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsCrate : MonoBehaviour, IDamageable
{

    public float soulsValue;

    private void Start()
    {
        soulsValue = SoulsSystem.instance.LostSouls;
        SoulsSystem.instance.LostSouls = 0;
    }

    public void GetDamage(float _damage)
    {
        Die();
    }

    public void Die()
    {
        SoulsSystem.instance.GainSouls(soulsValue);
        Destroy(this.gameObject);
    }

}
