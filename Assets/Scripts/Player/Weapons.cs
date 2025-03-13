using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapons : MonoBehaviour
{
    [Header(" The Default Attk")]   
    public float bAttackRateSec; // how often in seconds the attack is happening
    public float bAttackDMG; // how much dmg
    GameObject baseAttk;// base attk game object (their children should have the vfx and collider)
    Collider c1;   // to later add collider info into it 

    [Header(" weapon 2 Attk")] // might reuse or not 
    public float w2AttackRateSec;
    public float w2AttackDMG;
    GameObject weapon2;
    Collider c2;


    [Header(" weapon 3 Attk")]
    public float w3AttackRateSec;
    public float w3AttackDMG;
    GameObject weapon3;
    Collider c3;

    [Header("Health Settings")]
    VisualEffect bAttk;
    bool canAttk;
    void Start()
    {
        canAttk = true;

        baseAttk = GameObject.Find("BaseAttack");
        bAttk = baseAttk.GetComponentInChildren<VisualEffect>();
        c1 = baseAttk.GetComponentInChildren<Collider>();
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canAttk)
        {
            StartCoroutine(attckRate(bAttackRateSec));

        }
    }

    IEnumerator attckRate(float waitSeconds)
    {
        canAttk = false;

        bAttk.Play();
        // Debug.Log("Play vfx");

        yield return new WaitForSeconds(waitSeconds);
        bAttk.Stop();
       // Debug.Log("Stop vfx");
        canAttk = true;

        //yield return new WaitForSeconds(1f);



    }
}
