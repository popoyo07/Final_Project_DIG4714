using System;
using UnityEngine;

public enum EnemyWaveStates { BeginPhase, Preparing, Active, Cooldown }

public class EventManager : MonoBehaviour
{
    // Singleton instance
    public static EventManager Instance { get; private set; }
    public static event Action OnEventManagerReady;

    // The current state of the enemy waves
    public EnemyWaveStates CurrentState { get; private set; } = EnemyWaveStates.BeginPhase;

    // Event that is triggered when the state changes
    public event Action<EnemyWaveStates> OnStateChanged;

    [Header("Wave Settings")]
    public float waveIntervals = 75f; // Time between waves
    private float elapsedTime = 0f;

   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[EventManager] Instance initialized");
            OnEventManagerReady?.Invoke();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log($"[EventManager] Started with wave interval of {waveIntervals} seconds");
    }

    // Update is called once per frame
    public void Update()
    {
        elapsedTime += Time.deltaTime;

        // Handle state transitions based on current state
        switch (CurrentState)
        {
            case EnemyWaveStates.BeginPhase:
                if (elapsedTime > waveIntervals)
                {
                    ChangeState(EnemyWaveStates.Preparing);
                    elapsedTime = 0f;
                }
                break;
            case EnemyWaveStates.Preparing:
                break;
            case EnemyWaveStates.Active:
                break;
            case EnemyWaveStates.Cooldown:
                break;
        }
    }

    // Method to change the state and invoke the event
    public void ChangeState(EnemyWaveStates newState)
    {
        Debug.Log($"[EventManager] State changing from {CurrentState} to {newState}");
        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
    }
}
