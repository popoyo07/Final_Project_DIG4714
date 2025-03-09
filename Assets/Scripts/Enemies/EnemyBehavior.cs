using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public string typeOfEnemy;

    float speed;
    int health;
    int dmg;

    private PlayerController playerController;
    private NavMeshAgent agent;

    //prevent form overriding values 
    void Start()
    {
        typeOfEnemy = gameObject.tag;

        // add any componnets you need in here
        playerController = player.GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();

        speed = Enemy.theInstance.TheEnemySpeed(typeOfEnemy);
        health = Enemy.theInstance.TheEnemyHealth(typeOfEnemy);
        dmg = Enemy.theInstance.TheEnemyDMG(typeOfEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        Chase(speed);// use the variable inside the Dictionary
    }

    public void Chase(float enemySpeed)
    {
        agent.SetDestination(player.transform.position);
        agent.speed = enemySpeed;
        //Debug.Log("enemy speed is: " + enemySpeed);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            playerController.health -= dmg;
        }

        if(collision.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Enemy hit!");
            // Subtract enemy health by weapon's damage
        }
    }
}
