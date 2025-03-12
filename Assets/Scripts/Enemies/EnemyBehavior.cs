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
    private UIBars uiBars;

    //prevent form overriding values 
    void Start()
    {
        typeOfEnemy = gameObject.tag;

        // add any componnets you need in here
        playerController = player.GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        uiBars = player.GetComponent<UIBars>(); // Get UI reference

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

    public void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerController.health -= dmg;
            Debug.Log("Health:" + playerController.health);
            uiBars.LoseHealthBar(playerController.health);
            //Debug.Log("Enemy attacked! Player health reduced." + uiBars.currentHealth);
            Destroy(gameObject);
            uiBars.GainXPbar(2f);
            uiBars.GainUltBar(2f);
        }

        if(other.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Enemy hit!");
            // Subtract enemy health by weapon's damage
        }
    }
}
