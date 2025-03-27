using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponBehavior : MonoBehaviour
{
    public Collider c1;   // to later add collider info into it 
    bool canAttk;
    VisualEffect vfxAttk;
    VisualEffect vfxAttk2; //snowflakes
    public GameObject player;
    Weapons w;
    public float theDMG;
    string attk;
    public GameObject projectile;
    public Vector3 rotationAxis = Vector3.up;


    bool rotating;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        attk = gameObject.name;
        player = GameObject.FindWithTag("Player");
        w = player.GetComponent<Weapons>();
       
        vfxAttk = GetComponent<VisualEffect>();
        vfxAttk2 = GetComponent<VisualEffect>();
        c1 = GetComponent<Collider>();
        canAttk = true; 

        theDMG = w.WeaponDMG[attk];

     

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotating) 
        {
            transform.RotateAround(player.transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
        }

        if (canAttk)
        {
            StartCoroutine(AttckRate(w.WeaponRateSec[attk], attk, w.WeaponAttDuration[attk]));

        }
        theDMG = w.WeaponDMG[attk]; // the keep up to date in case it increases

    }
    IEnumerator AttckRate(float waitSeconds, string attackName, float attackDuuration)
    {
        switch (attackName)
        {
            case "AreaAttack":
                Attacking();
                break;

            case "BookAttack":
                Attacking();
                rotating = true; // enable rotation
                break;

            case "ProjectileAttack":

                break;
        }      

        yield return new WaitForSeconds(attackDuuration); // how long the attack will last 

        if (vfxAttk != null)
        {
            vfxAttk.Stop();
            vfxAttk2.Stop();
        }

        rotating = false; // desable rotation

        if(c1 != null)
        c1.enabled = false;

        yield return new WaitForSeconds(waitSeconds); // attack cool down 
        canAttk = true;



    }

    void Attacking()
    {
        canAttk = false;
        c1.enabled = true;
        Debug.Log(" The Collider for the weapon is " + c1.enabled);
        if (vfxAttk != null)
        {
            vfxAttk.Play();
            vfxAttk2.Play();

        }

    }
}
