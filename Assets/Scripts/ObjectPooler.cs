using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject[] objectPrefabs;  //kept it as arrays because random is better than not random
    public int initialPoolSize = 10; //how many swimming in pool
    private Dictionary<GameObject, Queue<GameObject>> objectPools = new Dictionary<GameObject, Queue<GameObject>>();

    private void Start()
    {
       
        foreach (GameObject prefab in objectPrefabs)
        {
            objectPools[prefab] = new Queue<GameObject>();

           
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objectPools[prefab].Enqueue(obj);
            }
        }
    }

    // this will get random object from pool
    public GameObject GetRandomPooledObject()
    {
        //thow prefab in pool
        GameObject selectedPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

        // if statement to make sure that there are prefabs swimming in the pool
        if (objectPools[selectedPrefab].Count > 0)
        {
            GameObject obj = objectPools[selectedPrefab].Dequeue();
            obj.SetActive(true);  // Activate the object before returning
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(selectedPrefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // when prefab esccape the pool, this will throw them back
    public void ThowBackToPool(GameObject obj)
    {
        obj.SetActive(false);  // deactivate so that they aren't running around in the pool(what I understood from object pool)

        
        foreach (var entry in objectPools)
        {
            if (entry.Key == obj)
            {
                entry.Value.Enqueue(obj);
                return;
            }
        }
    }
}
