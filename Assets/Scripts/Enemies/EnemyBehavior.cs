using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public string typeOfEnemy;

    float thisSpeed;
    float thisHelath;
    float thisDMG;

    private UIBars uiBars;

    //prevent form overriding values 
    void Start()
    {
        GettingComponents();

        thisSpeed = Enemy.theInstance.TheEnemySpeed(typeOfEnemy);
        thisHelath = Enemy.theInstance.TheEnemyHealth(typeOfEnemy);
        thisDMG = Enemy.theInstance.TheEnemyDMG(typeOfEnemy);

        uiBars = player.GetComponent<UIBars>(); // Get UI reference

    }

    // Update is called once per frame
    void Update()
    {
        Chase(thisSpeed);// use the variable inside the Dictionary
    }

    public void GettingComponents()
    {
        typeOfEnemy = gameObject.tag;

        // add any componnets you need in here
        player = GameObject.Find("Santa");
        agent = GetComponent<NavMeshAgent>();
       
    }

    public void Chase(float enemySpeed)
    {
        agent.SetDestination(player.transform.position);
        agent.speed = enemySpeed;
        Debug.Log("enemy speed is: " + enemySpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Ensure enemy only affects player
        {
            uiBars.LoseHealth(thisDMG);
            Debug.Log("Enemy attacked! Player health reduced." + uiBars.currentHealth);
            Destroy(gameObject);
            uiBars.GainXPbar(2f);
            uiBars.GainUltBar(2f);

        }
    }
}
