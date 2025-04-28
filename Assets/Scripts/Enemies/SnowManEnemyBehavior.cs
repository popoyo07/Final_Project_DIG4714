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
    [SerializeField] GameObject snowballVFXObject;
    public Transform spawnPoint;
    [SerializeField] private float stopDistance;
    private Weapons weapon;
    Rigidbody rb;


    Animator snowman_animator;
   
    [SerializeField] private float enemySpeed = 3f;
     private float health = 2;
    private UIBars uiBars;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        uiBars = player.GetComponent<UIBars>(); // Get UI reference

        snowman_animator = GetComponent<Animator>();
        weapon = player.GetComponent<Weapons>();

    }

    private void Start()
    {   //create game object that contains the snowball vfx attached that DamagingSnowball script can reference
        Instantiate(snowballVFXObject, new Vector3(0,1000,0), Quaternion.identity);
    }

    private void Update()
    {
        CheckHealth();
        snowballtime -= Time.deltaTime; //start timer 

        if ((player.transform.position - this.gameObject.transform.position).magnitude > stopDistance)
        {
            //if the distance between the player and this gameobject is greater than stopDistance, the enemy chases 
            Chase(enemySpeed);
        }
        if ((player.transform.position - this.gameObject.transform.position).magnitude <= stopDistance)
        {
            //if the distance between the player and this gameobject is less than stopDistance. Start attack
            snowman_animator.SetBool("isThrowing", true); //play animation by setting a boolean in animator to true
           // agent.SetDestination(transform.position);
            if (snowballtime <= 0) //if time has passed, attack 
            {
                   
                //Attack();
                Invoke("Attack", 2); //play attack method with a delay of 2 seconds
                snowballtime = timer; // set back to timer and start countdown again 
            }
        }

    }

    
    public void Attack()
    {

       
        GameObject snowball_clone = Instantiate(snowball, spawnPoint.transform.position, spawnPoint.transform.rotation);
        

        Destroy(snowball_clone, 1f);

        /* create a snowball clone at a position 
         * destory it after a certain amount of time if it hasn't hit anything 
         */
        
    }
    
    public void Chase(float speed)
    {
        snowman_animator.SetBool("isThrowing", false);
        Vector3 direction = (player.transform.position - transform.position).normalized; //direction the enemy goes
        rb.MovePosition(transform.position + direction * enemySpeed * Time.deltaTime); //actually moving the game object
        transform.LookAt(player.transform.position); // have this game object fix its rotation to look at the player
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
            }
        }
        /* if struck by a weapon
         * take health from the game object
         * if health is 0, create a coin in its place
         * add this game object to kill tracker 
         * destroy this game object 
         */
    }

    public void Add(GameObject g) //method for adding to list
    {
        KillTracker.killlist.Add(g);
    }
    void CheckHealth()
    {

        if (health <= 0)
        {
            Instantiate(coins, transform.position, transform.rotation); // instantiate a coin 
            uiBars.GainXPbar(5f);
            uiBars.GainUltBar(1f);
            Add(this.gameObject); //add the dead game object to the list
            Destroy(this.gameObject);
        }
    }


}