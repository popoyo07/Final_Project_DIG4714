using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static Enemy theInstance;
    

    [Header(" Elf Enemy")]
    public float elfHealth;
    public float elfDMG;
    public float elfSpeed;

    [Header(" Strong Elf Enemy")]
    public int strElfHealth;
    public float strElfDMG;
    public float strElfSpeed;

    // WIll add more enemies later 

    public Dictionary<string, float> EnemySpeed = new();  // Enemy Speed Dictionary
    public Dictionary<string, float> EnemyHealth = new(); // Enemy Health Dictionary
    public Dictionary<string, float> EnemyDMG = new(); // Enemy Damage Dictionary


    private void Awake()
    {
        theInstance = this;

        // For dictionary purposes the enemy tag shoudl be the same name as the Key.
        EnemySpeed["Elf"] = elfSpeed;
        EnemySpeed["StrongElf"] = strElfSpeed; 

        EnemyHealth["Elf"] = elfHealth;
        EnemyHealth["StrongElf"] = strElfHealth;

        EnemyDMG["Elf"] = elfDMG;
        EnemyDMG["StrongElf"] = strElfDMG;
    }
 
    public float TheEnemySpeed(string enemyType)
    {
        return EnemySpeed[enemyType];
    }
    public float TheEnemyHealth(string enemyType)
    {
        return EnemyHealth[enemyType];
    }
    public float TheEnemyDMG(string enemyType)
    {
        return EnemyDMG[enemyType];
    }



}
