using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UnityEvent onCursedObjectSpawned, onCursedObjectReset, onCursedObjectDestroyed;

    public List<GameObject> cursedObjects, destroyedCursedObjects;
    public GameObject currentCursedObject;
    public int cursedObjectsDestroyed = 0;

    public Locations cursedObjectSpawnLocations;

    void Start()
    {
        InitializeGameManager();
        SpawnCursedObject();
    }

    public void InitializeGameManager()
    {
        cursedObjectsDestroyed = 0;
        currentCursedObject = null;
        if(destroyedCursedObjects == null)
            destroyedCursedObjects = new();
    }

    public void SpawnCursedObject()
    {
        // If they have destroyed the object already.
        if(currentCursedObject != null)
            return;

        // Pick an object randomly
        GameObject nextCursedObject = cursedObjects[Random.Range(0, cursedObjects.Count)];
        
        // Don't randomly choose one that has been destroyed already
        while(destroyedCursedObjects.Contains(nextCursedObject))
            nextCursedObject = cursedObjects[Random.Range(0, cursedObjects.Count)];

        // Pick a spawn location randomly
        GameObject nextSpawnLocation = cursedObjectSpawnLocations.locations[Random.Range(0, cursedObjectSpawnLocations.locations.Count)];
        
        // Create the object at the location
        currentCursedObject = Instantiate(  nextCursedObject, 
                                            nextSpawnLocation.transform.position,
                                            nextSpawnLocation.transform.rotation, 
                                            transform);

        // Invoke the event
        onCursedObjectSpawned.Invoke();
    }

    public void DestroyCursedObject()
    {
        onCursedObjectDestroyed.Invoke();
    }

    public void ResetCursedObject()
    {
        // Stop it from falling or moving
        Rigidbody currentCursedObjectRB = currentCursedObject.GetComponent<Rigidbody>();
        currentCursedObjectRB.isKinematic = true;
        currentCursedObjectRB.velocity = Vector3.zero;

        // Put it at a spawn location
        currentCursedObject.transform.position = cursedObjectSpawnLocations.locations[Random.Range(0, cursedObjectSpawnLocations.locations.Count)].transform.position;
        
        // Invoke the function
        onCursedObjectReset.Invoke();
    }

    public void GetRandomSpawnPoint()
    {

    }
}
