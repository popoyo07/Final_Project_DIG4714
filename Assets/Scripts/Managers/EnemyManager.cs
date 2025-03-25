using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveSetting;

public class EnemyManager : MonoBehaviour
{
    [Header("Wave Settings:")]
    public List<WaveSetting> waves;
    private int currentWaveIndex = 0;

    private void OnEnable()
    {
        EventManager.Instance.OnStateChanged += HandleStateChange;
    }

    // BeginPhase, Preparing, Active, Cooldown
    private void HandleStateChange(EnemyWaveStates state) //using event manager states to change it
    {
        if (state == EnemyWaveStates.Preparing)
        {
            StartCoroutine(StartRush());
        }
    }
    
    private IEnumerator StartRush()
    {
        EventManager.Instance.ChangeState(EnemyWaveStates.Active);
        WaveData currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            SpawnEnemy(currentWave.enemyPrefab);
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }

        EventManager.Instance.ChangeState(EnemyWaveStates.Cooldown);
        yield return new WaitForSeconds(currentWave.cooldownTime);

        currentWaveIndex = (currentWaveIndex + 1) % waves.Count;
        EventManager.Instance.ChangeState(EnemyWaveStates.BeginPhase);

    }
    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 10f;
        spawnPosition.y = 0; // Ensure enemies spawn on the ground
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
