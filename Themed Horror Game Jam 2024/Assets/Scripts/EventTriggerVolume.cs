using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EventTriggerVolume : MonoBehaviour
{
    public GameObject player;
    public UnityEvent onPlayerEnter, onObjectEnter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            onPlayerEnter.Invoke();
        }
        else
        {
            onObjectEnter.Invoke();
        }
    }
}
