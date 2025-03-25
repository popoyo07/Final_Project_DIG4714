using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapons : MonoBehaviour
{
    [Header(" BaseAttk ")]   
    public float bAttackRateSec; // how often in seconds the attack is happening
    public float bAttackDMG; // how much dmg
    
    [Header(" weapon 2 Attk")] // might reuse or not 
    public float w2AttackRateSec;
    public float w2AttackDMG;
    //GameObject weapon2;
   // Collider c2;


    [Header(" weapon 3 Attk")]
    public float w3AttackRateSec;
    public float w3AttackDMG;
    // GameObject weapon3;
    //  Collider c3;

    [Header("Health Settings")]

    public Dictionary<string, float> WeaponDMG = new Dictionary<string, float>();
    public Dictionary<string, float> WeaponRateSec = new Dictionary<string, float>();
    void Awake()
    {
        WeaponDMG["BaseAttack"] = bAttackDMG;
        WeaponDMG["placeNameHere2"] = w2AttackDMG;
        WeaponDMG["PlaceNameHere3"] = w3AttackDMG;


        WeaponRateSec["BaseAttack"] = bAttackRateSec;
        WeaponRateSec["placeNameHere2"] = w2AttackRateSec;
        WeaponRateSec["placeNameHere3"] = w3AttackRateSec;

        Debug.Log("Weapon Rate for BaseAttack: " + WeaponRateSec["BaseAttack"]);
    }

}
