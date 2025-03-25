using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapons : MonoBehaviour
{
    [Header(" BaseAttk ")]   
    public float w1AttackRateSec; // how often in seconds the attack is happening
    public float w1AttackDMG; // how much dmg
    public float w1AttkDuration; // how long will the attack last
    
    [Header(" weapon 2 Attk")] // might reuse or not 
    public float w2AttackRateSec;
    public float w2AttackDMG;
    public float w2AttkDuration;
    //GameObject weapon2;
    // Collider c2;


    [Header(" weapon 3 Attk")]
    public float w3AttackRateSec;
    public float w3AttackDMG;
    public float w3AttkDuration;
    // GameObject weapon3;
    //  Collider c3;

    [Header("Health Settings")]

    public Dictionary<string, float> WeaponDMG = new Dictionary<string, float>();
    public Dictionary<string, float> WeaponRateSec = new Dictionary<string, float>();
    public Dictionary<string, float> WeaponAttDuration = new Dictionary<string, float>();
    void Awake()
    {
        //DMG
        WeaponDMG["BaseAttack"] = w1AttackDMG;
        WeaponDMG["BookAttack"] = w2AttackDMG;
        WeaponDMG["PlaceNameHere3"] = w3AttackDMG;

        // Rate in seconds
        WeaponRateSec["BaseAttack"] = w1AttackRateSec;
        WeaponRateSec["BookAttack"] = w2AttackRateSec;
        WeaponRateSec["placeNameHere3"] = w3AttackRateSec;

        // Attack duration
        WeaponAttDuration["BaseAttack"] = w1AttkDuration;
        WeaponAttDuration["BookAttack"] = w2AttkDuration;
        WeaponAttDuration["placeNameHere3"] = w3AttkDuration;

        Debug.Log("Weapon Rate for BaseAttack: " + WeaponRateSec["BaseAttack"]);
    }

}
