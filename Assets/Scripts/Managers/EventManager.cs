using System;
using UnityEngine;

//the different states the player will experience
public enum EnemyWaveStates { BeginPhase, Preparing, Active, Surrounding, Cooldown }

public class EventManager : MonoBehaviour
{
    
    public static EventManager Instance { get; private set; }
    public static event Action OnEventManagerReady; //so we know if the event is being subscribed to

    // The current state of the enemy waves Getting beginphase to start
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

    //This changes the events and invoke the begining of the new event
    public void ChangeState(EnemyWaveStates newState)
    {
        Debug.Log($"[EventManager] State changing from {CurrentState} to {newState}");
        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
    }
}