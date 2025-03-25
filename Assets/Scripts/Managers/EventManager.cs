using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using System;

public enum EnemyWaveStates { BeginPhase, Preparing, Active, Cooldown } // begin phase is the first few moments of time passing
                                                                    // preparing will build up, active is full cooldown is end

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public EnemyWaveStates CurrentState { get; private set; } = EnemyWaveStates.BeginPhase;
    public event Action<EnemyWaveStates> OnStateChanged;

    [Header("Wave Settings")]
    public float waveIntervals = 75f; //Time  between waves
    private float elapsedTime = 0f;

    private void Awake()
    {
        if (Instance != null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void Update()
    {
        elapsedTime += Time.deltaTime; //in game timer
        if (CurrentState == EnemyWaveStates.BeginPhase && elapsedTime > waveIntervals)
        {
            ChangeState(EnemyWaveStates.Preparing);
        }    
    }
    public void ChangeState(EnemyWaveStates newState)
    {
        OnStateChanged?.Invoke(newState);
    }    
}
