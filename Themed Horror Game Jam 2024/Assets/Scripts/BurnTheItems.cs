using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

public class BurnTheItems : MonoBehaviour
{
    public List<GameObject> objectsToBurn; //write in the specific name of the object
    private ParticleSystem fire;
    public UnityEvent onItemBurned;

    void Start()
    {
        fire = GameObject.Find("FireParticles").GetComponent<ParticleSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        for(int i = 0; i < objectsToBurn.Count; i++)
        {
            if(other.gameObject == objectsToBurn[i] && !other.gameObject.GetComponent<PickUpObject>().hasItem)
            {
                //Set your event or whatever you want here
                onItemBurned.Invoke();
                Destroy(other.gameObject);
                var main = fire.main;
                main.startSpeed = 3f;
                StartCoroutine(normalFire());
                break;
            }
        }

    }

    IEnumerator normalFire()
    {
        Debug.Log("Fire Check");
        // suspend execution for 1 seconds
        yield return new WaitForSeconds(2f);
        var main = fire.main;
        main.startSpeed = 1f;
        Debug.Log("Fire Check Complete");

    }
}
