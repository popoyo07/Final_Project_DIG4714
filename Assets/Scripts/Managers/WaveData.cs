using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "EnemyRush/WaveData")]
public class WaveData : ScriptableObject
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public int enemyCount = 10;
    public float spawnInterval = 1f;
    public float cooldownTime = 5f;

    [Header("Wave Type")]
    [Tooltip("If true, enemies will surround the player in a circle")]
    public bool isSurroundWave = false;
}
