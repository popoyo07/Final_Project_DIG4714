using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class enemySpawn : MonoBehaviour
{
    public Transform spawnpoint;
    public GameObject Enemy1;
    public GameObject Enemy2;

    public void Start()
    {
        
        StartCoroutine(Spawn());
        
    }


    IEnumerator Spawn()
    {
        while (true)
         {
            yield return new WaitForSeconds(3f);
            StartCoroutine(SpawnEnemy());

        } 
    }
    private IEnumerator SpawnEnemy()
    {
        float enemytype = UnityEngine.Random.Range(0, 1);

        yield return Instantiate(Enemy1, spawnpoint.position , Quaternion.identity);
    }
}