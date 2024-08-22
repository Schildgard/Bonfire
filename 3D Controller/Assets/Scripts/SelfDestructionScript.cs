using UnityEngine;

public class SelfDestructionScript : MonoBehaviour
{

    //This script is mostly used on temporary Effects like Status Conditions
    [SerializeField] private float lifeTime;
    private void OnEnable()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
