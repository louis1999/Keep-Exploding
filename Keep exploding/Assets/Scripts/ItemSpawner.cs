using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> prefabs;
    public List<GameObject> spawnedPrefabs;
    private List<GameObject> smallPrefabs;
    private List<GameObject> mediumPrefabs;
    private List<GameObject> bigPrefabs;

    void Start()
    {
        SeparateItems();
        SpawnItems();
    }

    void SeparateItems()
    {
        smallPrefabs = new List<GameObject>();
        mediumPrefabs = new List<GameObject>();
        bigPrefabs = new List<GameObject>();
        int counter = 0;
        foreach (GameObject prefab in prefabs)
        {
            
            print(counter);
            ItemSize size = prefab.GetComponent<Item>().size;
            if(size == ItemSize.SMALL)
            {
                smallPrefabs.Add(prefab);
            } else if(size == ItemSize.MEDIUM)
            {
                mediumPrefabs.Add(prefab);
            } else
            {
                
                bigPrefabs.Add(prefab);
            }
            counter++;
        }
    }
    void SpawnItems()
    {
        //spawnedPrefabs = new List<GameObject>();
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        foreach(Transform point in spawnPoints)
        {

            GameObject toSpawn = null;
            if (point.gameObject.tag == "SpawnPoint_Small")
            {
                int randomIndex = Random.Range(0, smallPrefabs.Count);
                toSpawn = Instantiate(smallPrefabs[randomIndex], point.localPosition, smallPrefabs[randomIndex].transform.rotation, gameObject.GetComponent<Transform>());
            }
            else if (point.gameObject.tag == "SpawnPoint_Medium")
            {
                
                int randomIndex = Random.Range(0, mediumPrefabs.Count);
                toSpawn = Instantiate(mediumPrefabs[randomIndex], point.localPosition, mediumPrefabs[randomIndex].transform.rotation, gameObject.GetComponent<Transform>());
            }
            else
            {
                print("BIGGGGGGGGGGGGG");
                int randomIndex = Random.Range(0, bigPrefabs.Count);
                toSpawn = Instantiate(bigPrefabs[randomIndex], point.localPosition, bigPrefabs[randomIndex].transform.rotation, gameObject.GetComponent<Transform>());
            }

            spawnedPrefabs.Add(toSpawn);
            
            
        }
        
    }

 
}
