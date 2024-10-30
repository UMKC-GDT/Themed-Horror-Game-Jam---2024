using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public List<Transform> rooms; // List of room spawn points
    // Correct items to spawn and filler items, for now there is 4 wrong items as to not litter the house with
    // too much wrong items.
    public GameObject correctItemPrefab1;
    public GameObject correctItemPrefab2;
    public GameObject correctItemPrefab3;
    public GameObject correctItemPrefab4;
    public GameObject fillerItemPrefab1;
    public GameObject fillerItemPrefab2;
    public GameObject fillerItemPrefab3;
    public GameObject fillerItemPrefab4;
    public int fillerItemCount = 8; // Number of filler items to spawn

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        // List of correct item prefabs
        List<GameObject> correctItemPrefabs = new List<GameObject>
        {
            correctItemPrefab1,
            correctItemPrefab2,
            correctItemPrefab3,
            correctItemPrefab4
        };
        List<GameObject> fillerItemPrefabs = new List<GameObject>
        {
            fillerItemPrefab1,
            fillerItemPrefab2,
            fillerItemPrefab3,
            fillerItemPrefab4
        };

        List<int> availableRooms = new List<int>();
        for (int i = 0; i < rooms.Count; i++) 
            availableRooms.Add(i);

        // Spawn each correct item in a unique room
        foreach (var correctPrefab in correctItemPrefabs)
        {
            int roomIndex = availableRooms[Random.Range(0, availableRooms.Count)];
            availableRooms.Remove(roomIndex); // Ensure no duplicate room for correct items
            Instantiate(correctPrefab, rooms[roomIndex].position, Quaternion.identity);
        }

        // Spawn filler items randomly across all rooms
        foreach (var fillerPrefab in fillerItemPrefabs)
        {
            int roomIndex = availableRooms[Random.Range(0, availableRooms.Count)];
            availableRooms.Remove(roomIndex); // Ensure no duplicate room for correct items
            Instantiate(fillerPrefab, rooms[roomIndex].position, Quaternion.identity);
        }
    }
}