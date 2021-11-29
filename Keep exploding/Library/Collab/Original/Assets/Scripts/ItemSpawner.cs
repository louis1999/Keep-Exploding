using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> prefabs;
    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        foreach(Transform point in spawnPoints)
        {
            int randomIndex = Random.Range(0, prefabs.Count);
            GameObject item = Instantiate(prefabs[randomIndex], point.localPosition, rot, gameObject.GetComponent<Transform>());
            
        }
        
    }

 
}
