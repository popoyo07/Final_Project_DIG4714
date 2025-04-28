using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject coins;
    public GameObject player;
    public string typeOfEnemy;
    public Weapons weapon;
    Animator elf_animator;

    // core stats for each enemy
    float speed;
    float health;
    float dmg;
    Rigidbody rb;

    private PlayerController playerController;
    //private NavMeshAgent agent;
    private UIBars uiBars;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        elf_animator = GetComponent<Animator>();
        typeOfEnemy = gameObject.tag;
        player = GameObject.FindWithTag("Player");

        // add any extra componnets you need in here
        playerController = player.GetComponent<PlayerController>();
        
        uiBars = player.GetComponent<UIBars>(); // Get UI reference
        weapon = player.GetComponent<Weapons>();
        
        speed = Enemy.theInstance.TheEnemySpeed(typeOfEnemy);
        health = Enemy.theInstance.TheEnemyHealth(typeOfEnemy);
        dmg = Enemy.theInstance.TheEnemyDMG(typeOfEnemy);


       


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // determine how far player is from the enemy 
        float distancePlayer = Vector3.Distance(transform.position, player.transform.position); 

        if (distancePlayer > 2f )
         Chase(speed);// use the variable inside the Enemy script attached to enemy manager 

            

          CheckHealth();// determines if enemy shoudl stay allive or die 

     }

    public void Chase(float enemySpeed)
    {
        // pull behavior 
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * enemySpeed * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }

   

    public void OnTriggerEnter (Collider other) //Attacking
    {
        //typeOfWeapon = other.name;
        if(other.gameObject.CompareTag("Player"))
        {
            playerController.health -= dmg;
            elf_animator.SetBool("isAttacking", true);
            //Debug.Log("Health:" + playerController.health);
            uiBars.LoseHealthBar(playerController.health);
            //Debug.Log("Enemy attacked! Player health reduced." + uiBars.currentHealth);
        }

        if(other.gameObject.CompareTag("Weapon"))
        {
            
            WeaponBehavior weaponBehavior = other.gameObject.GetComponent<WeaponBehavior>();
            Debug.Log("Remaining health: " + weaponBehavior.theDMG);
            if(weaponBehavior == null)    
            {

                Projectile p = other.gameObject.GetComponent<Projectile>();
                health -= p.dmg;
            }
            else if (weaponBehavior != null)
            {
                health -= weaponBehavior.theDMG;
                //Debug.Log("Remaining health: " + health);
                //checking if enemy is at 0 hp
                

            }


            if (health <= 0)
            {
                // isDead = true;
                Add(this.gameObject); //add the dead game object to the list

            }

            //Debug.Log("Enemy hit!");
            // Subtract enemy health by weapon's damage
        }

        
       
    }

    void CheckHealth()
    {

        if (health <= 0)
        {
            Instantiate(coins, transform.position, transform.rotation); // instantiate a coin 
            uiBars.GainXPbar(5f);
            uiBars.GainUltBar(1f);
            Destroy(this.gameObject);
        }
    }

    public void Add(GameObject g) //method for adding to list
    {
        KillTracker.killlist.Add(g); //reference to killlist found in Killtracker script
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // hange animations 
        {
            elf_animator.SetBool("isAttacking", false);
        }
    }



}
