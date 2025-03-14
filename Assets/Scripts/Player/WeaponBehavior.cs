using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponBehavior : MonoBehaviour
{
    public Collider c1;   // to later add collider info into it 
    bool canAttk;
    VisualEffect bAttk;
    public GameObject player;
    Weapons w;
    public float theDMG;
    string attk;

    Coroutine attackCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        attk = gameObject.name;
        player = GameObject.Find("Santa");
        w = player.GetComponent<Weapons>();
       
        bAttk = GetComponent<VisualEffect>();
        c1 = GetComponent<Collider>();
        canAttk = true; 

        theDMG = w.WeaponDMG[attk];

     

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (canAttk)
        {
            StartCoroutine(AttckRate(w.WeaponRateSec[attk]));

        }
        theDMG = w.WeaponDMG[attk];

    }
    IEnumerator AttckRate(float waitSeconds)
    {
        canAttk = false;
        c1.enabled = true;

        Debug.Log(" The Collider for the weapon is " + c1.enabled); 
        bAttk.Play();


        yield return new WaitForSeconds(0.3f); // how long the attack will last 

        bAttk.Stop();
        c1.enabled = false;

        yield return new WaitForSeconds(waitSeconds); // attack cool down 
        canAttk = true;

        Debug.Log(" The Collider for the weapon is " + c1.enabled);



    }
}
