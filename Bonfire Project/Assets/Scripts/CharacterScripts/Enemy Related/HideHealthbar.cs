using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHealthbar : MonoBehaviour
{
    private float timer;
    [SerializeField] private float activeTime;
    private void OnEnable()
    {
        timer = activeTime;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
       transform.LookAt(transform.position+ Camera.main.transform.forward);

        if (timer <= 0)
        {
            this.gameObject.SetActive(false);
            timer = activeTime;
        }
    }
}
