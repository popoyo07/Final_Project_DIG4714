using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnHere; 
    public GameObject[] enemies; //Array so that it is easy to add more variants to spawn

    private void Start()
    {
        StartCoroutine(SpawnRandomEnemy());
    }

    private IEnumerator SpawnRandomEnemy()
    {
        while (true) // will make it infinite
        {
            yield return new WaitForSeconds(6f);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemies.Length == 0)
        {
            Debug.LogWarning("ASSIGN ENEMY TO SPAWNER PREFAB IN INSPECTOR");
            return;
        }
        //use random so that we don't worry about specific spawner spawning one type of enemy
        int random = Random.Range(0, enemies.Length);
        Instantiate(enemies[random], spawnHere.position, Quaternion.identity);
    }
}
