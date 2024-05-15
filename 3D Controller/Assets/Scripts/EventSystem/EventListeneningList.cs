using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EventListeneningList : ScriptableObject
{

    [SerializeField] List <EventListener> Eventlisteners;
}
