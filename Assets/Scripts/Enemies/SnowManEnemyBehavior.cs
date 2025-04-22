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

    public int dmg = 1; // Damage projectile does
    public float shootForce = 100f; // how fast projectile will move

    [SerializeField] private float velocity = 1000.0f;
    [SerializeField] private float enemySpeed = 3f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        snowballtime -= Time.deltaTime;

        if ((player.transform.position - this.gameObject.transform.position).magnitude > 10)
        {
            // enemySpeed = 3;
            Chase(enemySpeed);
        }
        if ((player.transform.position - this.gameObject.transform.position).magnitude <= 10 && snowballtime <= 0)
        {
            // enemySpeed  = 0;
            snowballtime = timer;
            Attack();
        }

    }

    public void Attack()
    {
        agent.SetDestination((transform.position)); // Make the enemy stop moving and stand where it's at

        GameObject snowball_clone = Instantiate(snowball, spawnPoint.transform.position, spawnPoint.transform.rotation);
        DamagingSnowball snowballBullet = snowball.GetComponent<DamagingSnowball>(); // Get the bullet script on the projectile prefab
       // snowballBullet.Initialize(spawnPoint.position, shootForce, dmg); // Calls the function in the Bullet script to add force and damage to the projectile

        Destroy(snowball_clone, 2);

    }

    public void Chase(float speed)
    {
        agent.SetDestination(player.transform.position);
        agent.speed = speed;
    }





}