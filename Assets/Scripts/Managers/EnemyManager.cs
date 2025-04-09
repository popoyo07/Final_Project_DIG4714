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
        else if (state == EnemyWaveStates.Surrounding)
        {
            if (waves[currentWaveIndex].isSurroundWave)
            {
                SpawnSurroundingEnemies(waves[currentWaveIndex]);
            }
        }
        Debug.Log("State changed to: " + state);
    }

    private IEnumerator StartRush()
    {
        Debug.Log("[EnemyManager] Starting wave sequence");
        yield return new WaitForSeconds(2f); // Short preparation time

        WaveData currentWave = waves[currentWaveIndex];

        if (currentWave.isSurroundWave)
        {
            EventManager.Instance.ChangeState(EnemyWaveStates.Surrounding);
        }
        else
        {
            EventManager.Instance.ChangeState(EnemyWaveStates.Active);
            Debug.Log($"[EnemyManager] Starting wave {currentWaveIndex + 1} with {currentWave.enemyCount} enemies");

            for (int i = 0; i < currentWave.enemyCount; i++)
            {
                SpawnEnemy(currentWave.enemyPrefab);
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
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

    private void SpawnSurroundingEnemies(WaveData waveData)
    {
        if (playerTarget == null)
        {
            Debug.LogError("[EnemyManager] Player target not set! Please assign in Inspector");
            return;
        }

        Debug.Log($"[EnemyManager] Spawning {waveData.enemyCount} surrounding enemies");

        for (int i = 0; i < waveData.enemyCount; i++)
        {
            // Spawn enemies in a wider circle initially
            float angle = (360f / waveData.enemyCount) * i;
            float radian = angle * Mathf.Deg2Rad;
            Vector3 spawnPosition = transform.position + new Vector3(
                Mathf.Cos(radian) * 15f,
                0,
                Mathf.Sin(radian) * 15f
            );

            GameObject enemy = Instantiate(waveData.enemyPrefab, spawnPosition, Quaternion.identity);

            SurroundEnemyBehavior surroundBehavior = enemy.GetComponent<SurroundEnemyBehavior>();
            if (surroundBehavior != null)
            {
                surroundBehavior.player = playerTarget;
            }
        }

        // Move to cooldown after spawning all surrounding enemies
        StartCoroutine(WaitAndCooldown(waveData.cooldownTime));
    }

    private IEnumerator WaitAndCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        EventManager.Instance.ChangeState(EnemyWaveStates.Cooldown);
    }
}
