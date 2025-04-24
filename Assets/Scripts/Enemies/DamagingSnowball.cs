using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class DamagingSnowball : MonoBehaviour
{
    private PlayerController player;
    //private Transform PlayerPosition;
    Transform target;
    private UIBars uiBars;
    [SerializeField] private float snowball_dmg;

    [SerializeField] private float velocity;

    Rigidbody s_rigidbody;
    Transform snowball_spawner;
    Transform snowball_pos;
    Vector3 direction;

    //particle system
    GameObject snowburstGameObject;
    VisualEffect snowburst;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
       // PlayerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
        snowball_spawner = GameObject.FindWithTag("SpawnPoint").GetComponent<Transform>();
        
        uiBars = player.GetComponent<UIBars>();
        s_rigidbody = GetComponent<Rigidbody>();

        snowburstGameObject = GameObject.Find("SnowBurst");
        snowburst = snowburstGameObject.GetComponent<VisualEffect>();

        target = GameObject.Find("snowballtarget").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
         snowball_pos = this.gameObject.GetComponent<Transform>();
         direction = (target.position - snowball_spawner.position).normalized;   
        s_rigidbody.AddForce(direction * velocity, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.health -= snowball_dmg;
            uiBars.LoseHealthBar(player.health);

            snowburstGameObject.transform.position = snowball_pos.position;
            snowburst.Play();
            
            Destroy(this.gameObject);

        }
    }
    /*
     * draw a ray from the spawn point to the player 
     * if the ray hits the player collider 
     * launch the projectile towards the player 
     */
}
