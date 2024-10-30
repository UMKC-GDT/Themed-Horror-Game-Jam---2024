using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

public class BurnTheItems : MonoBehaviour
{
    public List<GameObject> objectsToBurn, wrongItemsToBurn;
    private ParticleSystem fire;
    public Color badFire, goodFire;
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
            var main = fire.main;
            main.startSpeed = 3f;
            main.startColor = goodFire;
            StartCoroutine(normalFire());
        }
        if(other.gameObject.CompareTag("NoBurn") && !other.gameObject.GetComponent<PickUpObject>().hasItem)
        {
            //Set your event or whatever you want here
            onWrongItemBurned.Invoke();
            Debug.Log("Incorrect");
            Destroy(other.gameObject);
            var main = fire.main;
            main.startSpeed = 3f;
            main.startColor = badFire;
            StartCoroutine(normalFire());
        }
    }

    IEnumerator normalFire()
    {
        Debug.Log("Fire Check");
        // suspend execution for 1 seconds
        yield return new WaitForSeconds(2f);
        var main = fire.main;
        main.startSpeed = 1f;
        main.startColor = badFire;
        Debug.Log("Fire Check Complete");
    }
}
