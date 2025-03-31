using System;
using UnityEngine;

public enum EnemyWaveStates { BeginPhase, Preparing, Active, Cooldown }

public class EventManager : MonoBehaviour
{
    // Singleton instance
    public static EventManager Instance { get; private set; }

    // The current state of the enemy waves
    public EnemyWaveStates CurrentState { get; private set; } = EnemyWaveStates.BeginPhase;

    // Event that is triggered when the state changes
    public event Action<EnemyWaveStates> OnStateChanged;

    [Header("Wave Settings")]
    public float waveIntervals = 75f; // Time between waves
    private float elapsedTime = 0f;

    // Ensure only one instance of the EventManager exists
    private void Awake()
    {
     
        {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
          
        }
        else
        {
           
            Destroy(gameObject);
        }
    }
}

    // Update is called once per frame
    public void Update()
    {
        elapsedTime += Time.deltaTime; // In-game timer

        // If the current state is BeginPhase and the wave interval has passed, change state to Preparing
        if (CurrentState == EnemyWaveStates.BeginPhase && elapsedTime > waveIntervals)
        {
            ChangeState(EnemyWaveStates.Preparing);
        }
    }

    // Method to change the state and invoke the event
    public void ChangeState(EnemyWaveStates newState)
    {
        CurrentState = newState;
        OnStateChanged?.Invoke(newState); // Trigger the state change event
    }
}
