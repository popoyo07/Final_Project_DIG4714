using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public string typeOfEnemy;

    int thisSpeed;
    int thisHelath;
    int thisDMG;


    //prevent form overriding values 
   void Start()
    {
        GettingComponents();

        thisSpeed = Enemy.theInstance.TheEnemySpeed(typeOfEnemy);
        thisHelath = Enemy.theInstance.TheEnemyHealth(typeOfEnemy);
        thisDMG = Enemy.theInstance.TheEnemyDMG(typeOfEnemy);


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
       // Debug.Log("Enemy " + EnemySpeed["Elf"]);
    }

    public void Chase(int enemySpeed)
    {
        agent.SetDestination(player.transform.position);
        agent.speed = enemySpeed;
        Debug.Log("enemy speed is: " + enemySpeed);
    }
}
