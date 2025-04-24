using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnowManEnemyBehavior : MonoBehaviour
{
    //a chase method that has the snowman chase the player
    //a attack method that has the snowman throw snowballs at the player
    //a switch between these two methods when the player is within the snowman's attack range

    private NavMeshAgent agent;
    public GameObject player;

    [SerializeField] private float timer = 5;
    private float snowballtime;

    public GameObject snowball;
    public Transform spawnPoint;
    [SerializeField] private float stopDistance;

    

   
    [SerializeField] private float enemySpeed = 3f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

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
            agent.SetDestination(transform.position);
            if (snowballtime <= 0)
            {
                Attack();
                snowballtime = timer;
            }
        }

    }

    
    public void Attack()
    {
        
       // agent.SetDestination((transform.position)); // Make the enemy stop moving and stand where it's at
        GameObject snowball_clone = Instantiate(snowball, spawnPoint.transform.position, spawnPoint.transform.rotation);
        

        Destroy(snowball_clone, 2.5f);
        
    }
    
    public void Chase(float speed)
    {
        agent.SetDestination(player.transform.position);
        agent.speed = speed;
    }





}