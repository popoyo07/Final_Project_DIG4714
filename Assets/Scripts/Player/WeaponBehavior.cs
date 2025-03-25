using System.Collections;
using System.Collections.Generic;
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
    public GameObject child;
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
            case "BaseAttack":
                Attacking();
                break;
            case "BookAttack":
                Attacking();
                rotating = true; // enable rotation

                Debug.Log("rotating around player");
                break;
        }      

        yield return new WaitForSeconds(attackDuuration); // how long the attack will last 

        if (vfxAttk != null)
        {
            vfxAttk.Stop();
        }

        rotating = false; // desable rotation 
        c1.enabled = false;

        yield return new WaitForSeconds(waitSeconds); // attack cool down 
        canAttk = true;

        Debug.Log(" The Collider for the weapon is " + c1.enabled);



    }

    void Attacking()
    {
        canAttk = false;
        c1.enabled = true;
        Debug.Log(" The Collider for the weapon is " + c1.enabled);
        if (vfxAttk != null)
        {
            vfxAttk.Play();

        }

    }
}
