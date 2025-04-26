using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject coins;
    public GameObject player;
    //public string typeOfWeapon;
    public string typeOfEnemy;
    public Weapons weapon;
    Animator elf_animator;

    // core stats for each enemy
    float speed;
    float health;
    int dmg;
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
        // add any componnets you need in here
        playerController = player.GetComponent<PlayerController>();
        //gent = GetComponent<NavMeshAgent>();
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
            Instantiate(coins, transform.position, transform.rotation);
            uiBars.GainXPbar(2f);
            uiBars.GainUltBar(2f);
            Destroy(this.gameObject);
        }
    }

    public void Add(GameObject g) //method for adding to list
    {
        KillTracker.killlist.Add(g);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            elf_animator.SetBool("isAttacking", false);
        }
    }



}
