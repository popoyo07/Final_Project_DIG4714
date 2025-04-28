using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponBehavior : MonoBehaviour
{
    public Collider c1;   // to later add collider info into it 
    bool canAttk;
    VisualEffect vfxAttk;
    public GameObject player;
    Weapons w;
    public float theDMG;
    string attk;
    public Vector3 rotationAxis = Vector3.up;

    [Header("projectile Weapon Spawner")]
    public GameObject Spear;
    public GameObject SomethingElse;


    bool rotating;
    public float rotationSpeed;

    [SerializeField] private AudioClip attkSoundClip;
    [SerializeField] private float weaponVolume;

    // Start is called before the first frame update
    void Awake()
    {
        attk = gameObject.name;
        player = GameObject.FindWithTag("Player");
        w = player.GetComponent<Weapons>();
       
        vfxAttk = GetComponent<VisualEffect>();
        c1 = GetComponent<Collider>();
        canAttk = true; 

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotating) // only enabled for 1 weapon
        {
            // rotates on player axis 
            transform.RotateAround(player.transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
        }

        if (canAttk)
        {
            canAttk = false;
            StartCoroutine(AttckRate(w.WeaponRateSec[attk], attk, w.WeaponAttDuration[attk]));

        }
        theDMG = w.WeaponDMG[attk]; // the keep up to date in case it increases

    }
    IEnumerator AttckRate(float waitSeconds, string attackName, float attackDuuration)
    {
        // takes in consideration the player name to determine which attack should do
        switch (attackName)
        {
            case "AreaAttack":
                Attacking();
                break;

            case "BookAttack":
                Attacking();
                rotating = true; // enable rotation
                break;
            case "SpikesAttack":
                Attacking();
                break;

            case "SpearAttack":
                Instantiate(Spear, transform.position, transform.rotation); // spawns the spear     
                break;
        }      

        yield return new WaitForSeconds(attackDuuration); // how long the attack will last 

        if (vfxAttk != null)
        {
            // stops vfx if aplicable 
            vfxAttk.Stop();
            vfxAttk.enabled = false;
        }

        rotating = false; // desable rotation

        if(c1 != null)
        {
            // desable collider if aplicable    
            c1.enabled = false;

        }

        yield return new WaitForSeconds(waitSeconds); // attack cool down 
        canAttk = true;

    }

    void Attacking()
    {
        // enable collidera and set dmg
        theDMG = w.WeaponDMG[attk]; // dmg is set here because it's value can be changed over time
        Debug.Log("It is attacking ");
        canAttk = false;
        c1.enabled = true;
       
        if (vfxAttk != null)
        {
            vfxAttk.enabled = true;
            
            AudioManager.instance.playSFX(attkSoundClip, transform, weaponVolume);
            vfxAttk.Play();
        
        }

    }

    void ProjectileSpawn()
    {
        if (Spear != null)
        {
            Instantiate(Spear);
            GameObject.Find("Spear").GetComponent<Projectile>().dmg = theDMG; // not fully optimal for future   
        }
        if (SomethingElse != null)
        {
            Instantiate(SomethingElse); // place holder name
        }

    }
}
