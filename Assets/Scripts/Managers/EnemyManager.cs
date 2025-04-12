using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Wave Settings:")]
    public List<WaveData> waves;
    private int currentWaveIndex = 0;

    [Header("Target Settings")]
    public GameObject playerTarget;

    private void Awake()
    {
        // Try to subscribe immediately if EventManager is already initialized 
        SubscribeToEventManager();

        // Also subscribe to the ready event in case EventManager isn't initialized yet
        EventManager.OnEventManagerReady += OnEventManagerReady;
    }

    private void OnEventManagerReady()
    {
        Debug.Log("[EnemyManager] EventManager is now ready, attempting to subscribe");
        SubscribeToEventManager();
    }

    private void SubscribeToEventManager()
    {
        if (EventManager.Instance != null)
        {
            Debug.Log("[EnemyManager] Successfully subscribed to EventManager events");
            EventManager.Instance.OnStateChanged += HandleStateChange;

            if (waves == null || waves.Count == 0)
            {
                Debug.LogError("[EnemyManager] No waves configured! Please set up waves in the Unity Inspector");
            }
        }
        else
        {
            Debug.LogWarning("[EnemyManager] Cannot subscribe yet - EventManager.Instance is still null");
        }
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnStateChanged -= HandleStateChange;
        }
        // Clean up the ready event subscription
        EventManager.OnEventManagerReady -= OnEventManagerReady;
    }

    // BeginPhase, Preparing, Active, Cooldown
    private void HandleStateChange(EnemyWaveStates state)
    {
        Debug.Log($"[EnemyManager] Received state change event: {state}");

        if (state == EnemyWaveStates.Preparing)
        {
            if (waves == null || waves.Count == 0)
            {
                Debug.LogError("[EnemyManager] Cannot start wave - no waves configured!");
                return;
            }

            if (waves[currentWaveIndex].enemyPrefab == null)
            {
                Debug.LogError($"[EnemyManager] Wave {currentWaveIndex + 1} has no enemy prefab assigned!");
                return;
            }

            Debug.Log($"[EnemyManager] Starting wave coroutine for wave {currentWaveIndex + 1}");
            StartCoroutine(StartRush());
        }
        Debug.Log("State changed to: " + state);
    }

    private IEnumerator StartRush()
    {
        Debug.Log("[EnemyManager] Starting wave sequence");
        yield return new WaitForSeconds(2f); // Short preparation time
        EventManager.Instance.ChangeState(EnemyWaveStates.Active);
        WaveData currentWave = waves[currentWaveIndex]; // Use WaveData to get properties

        Debug.Log($"[EnemyManager] Starting wave {currentWaveIndex + 1} with {currentWave.enemyCount} enemies");

        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            SpawnEnemy(currentWave.enemyPrefab);
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }

        Debug.Log($"[EnemyManager] Wave {currentWaveIndex + 1} complete, entering cooldown");
        EventManager.Instance.ChangeState(EnemyWaveStates.Cooldown);
        yield return new WaitForSeconds(currentWave.cooldownTime);

        Debug.Log($"[EnemyManager] Cooldown complete, moving to next wave");
        currentWaveIndex = (currentWaveIndex + 1) % waves.Count; // Move to the next wave
        EventManager.Instance.ChangeState(EnemyWaveStates.BeginPhase);
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (playerTarget == null)
        {
            Debug.LogError("[EnemyManager] Player target not set! Please assign in Inspector");
            return;
        }

        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 10f;
        spawnPosition.y = 0;
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
        if (enemyBehavior != null)
        {
            enemyBehavior.player = playerTarget;
        }
    }
}
