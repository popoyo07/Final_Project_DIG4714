using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject[] AllObjects;
    public GameObject nearestEnemy;
    float distance;
   // float nearestDistance = 1000;

    public float dmg;
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("SpearAttack");
        dmg = weapon.GetComponent<WeaponBehavior>().theDMG;
        FindNearestEnemy();

        if (nearestEnemy != null)
        {
            StartCoroutine(moveToTarget());
        }

        StartCoroutine(waitToDstroy(1.5f));

    }

    private void FindNearestEnemy()
    {
        int enemyLayer = LayerMask.GetMask("Enemy"); // Make sure the Enemy Layer exists
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1000f, enemyLayer); // Adjust range as needed

        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = collider.gameObject;
            }
        }
    }

   private IEnumerator moveToTarget()
    {
        while (nearestEnemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nearestEnemy.transform.position, 10f * Time.deltaTime);
            if (Vector3.Distance(transform.position, nearestEnemy.transform.position) < 0.1f)
            {
               
                Destroy(gameObject);
            }
            yield return null;
        }
    }
    IEnumerator waitToDstroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }


}
