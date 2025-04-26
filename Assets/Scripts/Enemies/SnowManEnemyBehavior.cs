using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnowManEnemyBehavior : MonoBehaviour
{
    //a chase method that has the snowman chase the player
    //a attack method that has the snowman throw snowballs at the player
    //a switch between these two methods when the player is within the snowman's attack range

  //  private NavMeshAgent agent;
    public GameObject player;
    public GameObject coins;

    [SerializeField] private float timer = 5;
    private float snowballtime;

    public GameObject snowball;
    public Transform spawnPoint;
    [SerializeField] private float stopDistance;
    private Weapons weapon;
    Rigidbody rb;


    Animator snowman_animator;
   
    [SerializeField] private float enemySpeed = 3f;
     private float health = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
       // agent = GetComponent<NavMeshAgent>();
        snowman_animator = GetComponent<Animator>();
        weapon = player.GetComponent<Weapons>();

    }

    private void Update()
    {
        snowballtime -= Time.deltaTime;

        if ((player.transform.position - this.gameObject.transform.position).magnitude > stopDistance)
        {
            
            Chase(enemySpeed);
        }
        if ((player.transform.position - this.gameObject.transform.position).magnitude <= stopDistance)
        {
            snowman_animator.SetBool("isThrowing", true);
           // agent.SetDestination(transform.position);
            if (snowballtime <= 0)
            {
                   
                //Attack();
                Invoke("Attack", 2);
                snowballtime = timer;
            }
        }

    }

    
    public void Attack()
    {

       
        GameObject snowball_clone = Instantiate(snowball, spawnPoint.transform.position, spawnPoint.transform.rotation);
        

        Destroy(snowball_clone, 2.5f);
        
    }
    
    public void Chase(float speed)
    {
        snowman_animator.SetBool("isThrowing", false);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * enemySpeed * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {

            WeaponBehavior weaponBehavior = other.gameObject.GetComponent<WeaponBehavior>();
             Debug.Log("Remaining health: " + weaponBehavior.theDMG);
            if (weaponBehavior != null)
            {

                health -= weaponBehavior.theDMG;
                if (health == 0)
                {
                    Debug.Log("the snowman is dead");
                    Instantiate(coins, transform.position, transform.rotation);

                    Add(this.gameObject); //add the dead game object to the list
                    Destroy(this.gameObject);

                }
            }
        }
    }

    public void Add(GameObject g) //method for adding to list
    {
        KillTracker.killlist.Add(g);
    }



}