using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmArea : MonoBehaviour
{
    [SerializeField] private GameEvent enterAreaEvent;
    [SerializeField] private GameEvent exitAreaEvent;
    //[SerializeField] private int audioManagerMusicIndex;


    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            enterAreaEvent.Raise();
           // AudioManager.instance.ChangeBackGroundMusic(audioManagerMusicIndex);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            exitAreaEvent.Raise();
            //AudioManager.instance.ChangeBackGroundMusic(0);
        }
    }
}
