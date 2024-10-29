using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Locations : MonoBehaviour
{
    public List<GameObject> locations;

    public void PopulateFromChildObjects()
    {
        if(locations == null)
            locations = new();

        locations.Clear();

        foreach(Transform child in transform)
        {
            locations.Add(child.gameObject);
        }
    }
}
