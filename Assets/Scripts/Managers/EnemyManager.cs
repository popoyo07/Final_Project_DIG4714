using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Wave Settings:")]
    public List<WaveData> waves;
    private int currentWaveIndex = 0;

    [Header("Target Settings")] //assign player tag parent
    public GameObject playerTarget;

    [Header("Object Pool Settings")]
    public ObjectPooler enemyPool;  // new and improved objecct pooooler

    private void Awake()
    {
        SubscribeToEventManager();

      
        EventManager.OnEventManagerReady += OnEventManagerReady;
    }

    private void OnEventManagerReady()
    {
        Debug.Log("[EnemyManager] EventManager is now ready, attempting to subscribe");
        SubscribeToEventManager();
    }

    private void SubscribeToEventManager()
    {
        //Event manager was null and kept deleting. Had to search beyond to find out how to make sure they stay.
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

    private void HandleStateChange(EnemyWaveStates state)
    {
        Debug.Log($"[EnemyManager] : {state}");

        if (state == EnemyWaveStates.Preparing)
        {
            if (waves == null || waves.Count == 0)
            {
                Debug.LogError("[EnemyManager] Cannot start wave cause you forgot to attach the wavedata to the manager!!!!");
                return;
            }

            if (waves[currentWaveIndex].enemyPrefab == null)
            {
                Debug.LogError($"[EnemyManager] Wave {currentWaveIndex + 1} have wavedata but no wavedata assets, ASSIGN PREFAB TO THE WAVE DATA ASSETS ANY QUESTIONS ASK TIGER!");
                return;
            }

          
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
        yield return new WaitForSeconds(2f); // Short preparation time so that player doesn't wait forever for enemies to come

        WaveData currentWave = waves[currentWaveIndex];

        if (currentWave.isSurroundWave)
        {
            EventManager.Instance.ChangeState(EnemyWaveStates.Surrounding); //change enemy state to a surround wave. This is just a single state that slowly has enemy walk to player
        }
        else
        {
            EventManager.Instance.ChangeState(EnemyWaveStates.Active);
            Debug.Log($"[EnemyManager] Starting wave {currentWaveIndex + 1} with {currentWave.enemyCount} enemies");

            for (int i = 0; i < currentWave.enemyCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
        }

        Debug.Log($"[EnemyManager] Wave {currentWaveIndex + 1} complete");
        EventManager.Instance.ChangeState(EnemyWaveStates.Cooldown);
        yield return new WaitForSeconds(currentWave.cooldownTime);

        Debug.Log($"[EnemyManager] Cooldown complete");
        currentWaveIndex = (currentWaveIndex + 1) % waves.Count; // Move to the next wave
        EventManager.Instance.ChangeState(EnemyWaveStates.BeginPhase);
    }

    private void SpawnEnemy()
    {
        if (playerTarget == null)
        {
            Debug.LogError("[EnemyManager] Player target not set! Please assign in Inspector");
            return;
        }

        // Adjusted to use Object pooler for spawning enemioes
        GameObject enemy = enemyPool.GetRandomPooledObject();
        if (enemy == null) return;

        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 150f; //150 so that it will always be far away from the camera (Small chance to spawn near player though)
        spawnPosition.y = 0;
        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = Quaternion.identity;

      
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
            Debug.LogError("[EnemyManager] Player target not set! Please assign in Inspector"); //this should already automatically find the player at start, but just in case
            return;
        }

        Debug.Log($"[EnemyManager] Spawning {waveData.enemyCount} surrounding enemies");

        for (int i = 0; i < waveData.enemyCount; i++)
        {
            // make the surrounding enemies spawn farther away!! can't figure out the movement speed won't change (SOLVED MIGUEL'S ENEMY LIST HAD PREASSIGNED MOVE SPEED)
            float angle = (360f / waveData.enemyCount) * i;
            float radian = angle * Mathf.Deg2Rad;
            Vector3 spawnPosition = transform.position + new Vector3(
                Mathf.Cos(radian) * 50f,
                0,
                Mathf.Sin(radian) * 50f
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


