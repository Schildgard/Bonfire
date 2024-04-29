using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimationHandling : MonoBehaviour
{

    //[SerializeField] Animation Animation;
    [SerializeField] Animator Animator;
    [SerializeField] PlayerActionScript PlayerActionScript;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerWalkAnimation() 
    {

    }
}
