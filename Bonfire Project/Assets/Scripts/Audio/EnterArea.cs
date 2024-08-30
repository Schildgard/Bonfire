using UnityEngine;

public class EnterArea : MonoBehaviour
{

    //When entering or exit the area a event is called which most likely triggers a bgm/sfx to play or blend to Area font
    [SerializeField] private GameEvent enterAreaEvent;
    [SerializeField] private GameEvent exitAreaEvent;


    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            enterAreaEvent.Raise();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            exitAreaEvent.Raise();
        }
    }
}
