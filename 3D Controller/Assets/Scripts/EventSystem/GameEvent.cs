using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent :ScriptableObject
{
     public List<EventListener> Listeners = new List<EventListener>();

    public void Raise() 
    {

        for (int i = 0; i < Listeners.Count; i++) 
        {
            Listeners[i].OnEventRaise();
        }

    }

    public void RegisterListener(EventListener _listener)
    {
        if (!Listeners.Contains(_listener)) 
        {
        Listeners.Add(_listener);

        }
    }

    public void UnRegisterListener(EventListener _listener) 
    {
        if (Listeners.Contains(_listener)) 
        {
        Listeners.Remove(_listener);

        }
    }
}
