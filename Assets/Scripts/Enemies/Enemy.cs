using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static Enemy theInstance; // reference the instance on scripts when you need to acces to this info 

    // the idea is to be able to set all this values on inspector,
    // so we can keep track of the enemy stars more easily 
    [Header("Snow Man Enemy")]
    public int SnowManHealth;
    public int SnowManDMG;
    public int SnowManSpeed;
    

    [Header(" Elf Enemy")]
    public int elfHealth;
    public int elfDMG;
    public float elfSpeed;

    [Header(" Strong Elf Enemy")]
    public int strElfHealth;
    public int strElfDMG;
    public float strElfSpeed;

    // WIll add more enemies later 

    public Dictionary<string, float> EnemySpeed = new();  // Enemy Speed Dictionary
    public Dictionary<string, int> EnemyHealth = new(); // Enemy Health Dictionary
    public Dictionary<string, int> EnemyDMG = new(); // Enemy Damage Dictionary


    private void Awake()
    {
        theInstance = this;

        // For dictionary purposes the enemy tag shoudl be the same name as the Key.
        EnemySpeed["Elf"] = elfSpeed;
        EnemySpeed["StrongElf"] = strElfSpeed;
        EnemySpeed["Snowman"] = SnowManSpeed;

        EnemyHealth["Elf"] = elfHealth;
        EnemyHealth["StrongElf"] = strElfHealth;
        EnemyHealth["Snowman"] = SnowManHealth;

        EnemyDMG["Elf"] = elfDMG;
        EnemyDMG["StrongElf"] = strElfDMG;
        EnemyDMG["Snowman"] = SnowManDMG;


    }
    

    public float TheEnemySpeed(string enemyType) // get enemy speed 
    {
        return EnemySpeed[enemyType];
    }
    public int TheEnemyHealth(string enemyType)// get enemy Helth 
    {
        return EnemyHealth[enemyType];
    }
    public int TheEnemyDMG(string enemyType) // get enemy DMG
    {
        return EnemyDMG[enemyType];
    }



}
