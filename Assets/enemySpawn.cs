using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;

    [Header("Spawn Settings")]
    public float minSpawnDistance = 15f; // Minimum distance from camera to spawn
    public float maxSpawnDistance = 25f; // Maximum distance from camera to spawn
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No camera on ze script");
            return;
        }

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        if (mainCamera == null) yield break;

        // Get a random position outside camera view
        Vector3 spawnPosition = GetSpawnPositionOutsideCamera();

        // Randomly choose between Enemy1 and Enemy2
        GameObject enemyToSpawn = Random.value < 0.5f ? Enemy1 : Enemy2;

        yield return Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetSpawnPositionOutsideCamera()
    {
        // Gets a random angle 
        float randomAngle = Random.Range(0f, 360f);
        // Get random distance between min and max spawn distance
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        // referencing camera
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 spawnDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;
        Vector3 spawnPosition = cameraPosition + (spawnDirection * randomDistance);

        // Keep the Y position at ground level for navmesh I hope this works
        spawnPosition.y = 0;

        return spawnPosition;
    }
}