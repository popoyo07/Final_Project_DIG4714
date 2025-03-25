using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSetting : MonoBehaviour
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "EnemyRush/WaveData")]
public class WaveData : ScriptableObject
    {
        [Header("Enemy Settings")]
        public GameObject enemyPrefab;
        public int enemyCount = 10;
        public float spawnInterval = 1f;
        public float cooldownTime = 5f;

        public static implicit operator WaveData(WaveSetting v)
        {
            throw new NotImplementedException();
        }
    }
}