using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject[] AllObjects;
    public GameObject nearestEnemy;
    float distance;
    float nearestDistance = 1000;

    public float dmg;
    public WeaponBehavior weapon;
    // Start is called before the first frame update
    void Start()
    {
        AllObjects = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < AllObjects.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, AllObjects[i].transform.position);

            if (distance < nearestDistance)
            {
                nearestEnemy = AllObjects[i];
                nearestDistance = distance;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
