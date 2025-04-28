using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class DamagingSnowball : MonoBehaviour
{
    private PlayerController player; //player ref
    //private Transform PlayerPosition;
    Transform target; //where the snowball will go 
    private UIBars uiBars; // ui bar ref

    //stats 
    [SerializeField] private float snowball_dmg;

    [SerializeField] private float velocity;

    //rigid body and transforms and vectors 
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

        

        target = GameObject.Find("snowballtarget").GetComponent<Transform>();

        //try to find objects and componenets in the scene 
    }

    private void Start()
    {
        snowburstGameObject = GameObject.Find("SnowBurst(Clone)");
        snowburst = snowburstGameObject.GetComponent<VisualEffect>();

        //find the snowBurst clone instantiated by SnowManEnemyBehavior
        //and get its componenet Visual Effect 
    }

    private void FixedUpdate()
    {
         snowball_pos = this.gameObject.GetComponent<Transform>();
        if (snowball_pos != null)
        {
            direction = (target.position - snowball_spawner.position).normalized;
            s_rigidbody.AddForce(direction * velocity, ForceMode.Impulse);
        }

        /* get the snowball's position 
         * the direction is between the target position and the spawn position (the snowman's hand)
         * add a force to the snowball
         */ 
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
     * if the snowball's collider enters the player's trigger 
     * the player loses health based on snowball's damage 
     * affects health bar
     * the visual effect appears where the snowball's position is 
     * play the visual effect
     * destroy the game object 
     */
}
