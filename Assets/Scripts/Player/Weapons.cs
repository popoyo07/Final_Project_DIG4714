using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapons : MonoBehaviour
{
    [Header(" Area Attk ")]   
    public float w1AttackRateSec; // how often in seconds the attack is happening
    public float w1AttackDMG; // how much dmg
    public float w1AttkDuration; // how long will the attack last
    
    [Header(" Snow flake Attk")] // might reuse or not 
    public float w2AttackRateSec;
    public float w2AttackDMG;
    public float w2AttkDuration;
    //GameObject weapon2;
    // Collider c2;


    [Header(" Projectile Attk")]
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
        WeaponDMG["AreaAttack"] = w1AttackDMG;
        WeaponDMG["BookAttack"] = w2AttackDMG;
        WeaponDMG["ProjectileAttack"] = w3AttackDMG;

        // Rate in seconds
        WeaponRateSec["AreaAttack"] = w1AttackRateSec;
        WeaponRateSec["BookAttack"] = w2AttackRateSec;
        WeaponRateSec["ProjectileAttack"] = w3AttackRateSec;

        // Attack duration
        WeaponAttDuration["AreaAttack"] = w1AttkDuration;
        WeaponAttDuration["BookAttack"] = w2AttkDuration;
        WeaponAttDuration["ProjectileAttack"] = w3AttkDuration;
    }

}
