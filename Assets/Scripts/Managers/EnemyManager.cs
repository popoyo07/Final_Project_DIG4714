using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Wave Settings:")]
    public List<WaveData> waves; // 
    private int currentWaveIndex = 0;

    private void OnEnable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnStateChanged += HandleStateChange;
        }
        else
        {
            Debug.LogWarning("EventManager.Instance is null");
        }
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnStateChanged -= HandleStateChange;
        }
    }

    // BeginPhase, Preparing, Active, Cooldown
    private void HandleStateChange(EnemyWaveStates state)
    {
        if (state == EnemyWaveStates.Preparing)
        {
            StartCoroutine(StartRush());
        }
        Debug.Log("State changed to: " + state);
    }

    private IEnumerator StartRush()
    {
        EventManager.Instance.ChangeState(EnemyWaveStates.Active);
        WaveData currentWave = waves[currentWaveIndex]; // Use WaveData to get properties

        Debug.Log($"Starting wave {currentWaveIndex + 1} with {currentWave.enemyCount} enemies");

        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            SpawnEnemy(currentWave.enemyPrefab);
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }

        EventManager.Instance.ChangeState(EnemyWaveStates.Cooldown);
        yield return new WaitForSeconds(currentWave.cooldownTime);

        Debug.Log($"Wave {currentWaveIndex + 1} complete, moving to next wave");

        currentWaveIndex = (currentWaveIndex + 1) % waves.Count; // Move to the next wave
        EventManager.Instance.ChangeState(EnemyWaveStates.BeginPhase);
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 10f;
        spawnPosition.y = 0; // Ensure enemies spawn on the ground
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
