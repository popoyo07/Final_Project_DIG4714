using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponBehavior : MonoBehaviour
{

   
    SphereCollider c1;   // to later add collider info into it 
    bool canAttk;
    VisualEffect bAttk;
    public GameObject player;
    Weapons w;
    public float theDMG;
    string attk;

    // Start is called before the first frame update
    void Awake()
    {
        attk = gameObject.name;
        player = GameObject.Find("Santa");
        w = player.GetComponent<Weapons>();
       
        bAttk = GetComponentInChildren<VisualEffect>();
        c1 = GetComponentInChildren<SphereCollider>();
        canAttk = true;

        theDMG = w.WeaponDMG[attk];

        Debug.Log("Weapon name: " + attk); // Debugging output
        Debug.Log("Weapon Damage: " + theDMG); // Debugging output
        Debug.Log("Player name: " + player.name);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (canAttk)
        {
            StartCoroutine(attckRate(w.WeaponRateSec[attk]));

        }
        theDMG = w.WeaponDMG[attk];

    }
    IEnumerator attckRate(float waitSeconds)
    {
        canAttk = false;
        c1.enabled = true;

        bAttk.Play();
        Debug.Log("Play vfx");
        
        yield return new WaitForSeconds(waitSeconds);
        bAttk.Stop();
        Debug.Log("Stop vfx");
        canAttk = true;
        c1.enabled = false;

        //yield return new WaitForSeconds(1f);



    }
}
