using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DamagingSnowball : MonoBehaviour
{
    private PlayerController player;
    private Transform PlayerPosition;
    private UIBars uiBars;
    [SerializeField] private float snowball_dmg;

    [SerializeField] private float velocity;

    Rigidbody s_rigidbody;
    Transform snowball_spawner;
    Vector3 direction;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        PlayerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
        snowball_spawner = GameObject.Find("snowball_spawner").GetComponent<Transform>();
        
        uiBars = player.GetComponent<UIBars>();
        s_rigidbody = GetComponent<Rigidbody>();

        
        
    }

    private void FixedUpdate()
    {
       direction = (PlayerPosition.position - snowball_spawner.position).normalized;   
        s_rigidbody.AddForce(direction * velocity, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.health -= snowball_dmg;
            uiBars.LoseHealthBar(player.health);
            
            Destroy(this.gameObject);
        }
    }
    /*
     * draw a ray from the spawn point to the player 
     * if the ray hits the player collider 
     * launch the projectile towards the player 
     */
}
