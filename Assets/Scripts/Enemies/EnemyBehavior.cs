using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    //public string typeOfWeapon;
    public string typeOfEnemy;
    public Weapons weapon;

    // core stats for each enemy
    float speed;
    float health; 
    int dmg;

    private PlayerController playerController;
    private NavMeshAgent agent;
    private UIBars uiBars;

    void Start()
    {
        typeOfEnemy = gameObject.tag;

        // add any componnets you need in here
        playerController = player.GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        uiBars = player.GetComponent<UIBars>(); // Get UI reference
        weapon = player.GetComponent<Weapons>();
        
        speed = Enemy.theInstance.TheEnemySpeed(typeOfEnemy);
        health = Enemy.theInstance.TheEnemyHealth(typeOfEnemy);
        dmg = Enemy.theInstance.TheEnemyDMG(typeOfEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        Chase(speed);// use the variable inside the Dictionary
        CheckHealth();
    }

    public void Chase(float enemySpeed)
    {
        agent.SetDestination(player.transform.position);
        agent.speed = enemySpeed;
        //Debug.Log("enemy speed is: " + enemySpeed);
    }

    public void OnTriggerEnter (Collider other)
    {
        //typeOfWeapon = other.name;
        if(other.gameObject.CompareTag("Player"))
        {
            playerController.health -= dmg;
            Debug.Log("Health:" + playerController.health);
            uiBars.LoseHealthBar(playerController.health);
            //Debug.Log("Enemy attacked! Player health reduced." + uiBars.currentHealth);
            uiBars.GainXPbar(2f);
            uiBars.GainUltBar(2f);
        }

        if(other.gameObject.CompareTag("Weapon"))
        {
            
            WeaponBehavior weaponBehavior = other.gameObject.GetComponent<WeaponBehavior>();
            Debug.Log("Remaining health: " + weaponBehavior.theDMG);
            if (weaponBehavior != null)
            {
                health -= weaponBehavior.theDMG;
                Debug.Log("Remaining health: " + health);
            }
            else
            {

                Projectile p = other.gameObject.GetComponent<Projectile>();
                health -= p.dmg;
            }
            


            Debug.Log("Enemy hit!");
            // Subtract enemy health by weapon's damage
        }

        
       
    }

    void CheckHealth()
    {

        if (health <= 0)
        Destroy(this.gameObject);
        

    }
}
