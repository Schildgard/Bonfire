using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public GameEvent Event;

    public UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);

     //  if (Event.Listeners.Contains(this)) 
     //  {
     //  Debug.Log($"{gameObject.name} has been registered as Listener to {Event.name}");
     //
     //  }
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaise() 
    {
        Response.Invoke();
    }
}
