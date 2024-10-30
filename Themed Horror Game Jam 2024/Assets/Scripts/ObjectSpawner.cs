using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public List<Transform> rooms; // List of room spawn points
    public List<GameObject> correctItems; //List of correct items to spawn
    public List<GameObject> fillerItems; //List of filler items to spawn

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {

        List<int> availableRooms = new List<int>();
        for (int i = 0; i < rooms.Count; i++) 
            availableRooms.Add(i);

        // Spawn each correct item in a random room first, and delete that spawn position as a possible
        // spawn position to not cause duplicate spawning.
        foreach (var correctPrefab in correctItems)
        {
            int roomIndex = availableRooms[Random.Range(0, availableRooms.Count)];
            availableRooms.Remove(roomIndex); // Ensure no duplicate room for correct items
            Instantiate(correctPrefab, rooms[roomIndex].position, Quaternion.identity);
        }

        // Spawn filler items randomly across the rooms leftover
        foreach (var fillerPrefab in fillerItems)
        {
            int roomIndex = availableRooms[Random.Range(0, availableRooms.Count)];
            availableRooms.Remove(roomIndex); // Ensure no duplicate room for correct items
            Instantiate(fillerPrefab, rooms[roomIndex].position, Quaternion.identity);
        }
    }
}