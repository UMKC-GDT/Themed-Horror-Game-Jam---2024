using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

public class BurnTheItems : MonoBehaviour
{
    public List<GameObject> objectsToBurn, wrongItemsToBurn;
    public ParticleSystem fire, goodFire;
    public UnityEvent onItemBurned, onWrongItemBurned;

    void Start()
    {
        fire = GameObject.Find("FireParticles").GetComponent<ParticleSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Burn") && !other.gameObject.GetComponent<PickUpObject>().hasItem)
        {
            //Set your event or whatever you want here
            onItemBurned.Invoke();
            Debug.Log("Correct");
            Destroy(other.gameObject);
            goodFire.Play();
            fire.Stop();
            var main = fire.main;
            main.startSpeed = 3f;
            StartCoroutine(normalFire());
        }
        if(other.gameObject.CompareTag("NoBurn") && !other.gameObject.GetComponent<PickUpObject>().hasItem)
        {
            //Set your event or whatever you want here
            onWrongItemBurned.Invoke();
            Debug.Log("Incorrect");
            Destroy(other.gameObject);
            fire.Play();
            var main = fire.main;
            main.startSpeed = 3f;
            StartCoroutine(normalFire());
        }
    }

    IEnumerator normalFire()
    {
        Debug.Log("Fire Check");
        // suspend execution for 1 seconds
        yield return new WaitForSeconds(2f);
        goodFire.Stop();
        fire.Play();
        var main = fire.main;
        main.startSpeed = 1f;
        Debug.Log("Fire Check Complete");
    }
}
