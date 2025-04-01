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

    [Header(" Projectile Attk")]
    public float w3AttackRateSec;
    public float w3AttackDMG;
    public float w3AttkDuration;
    // GameObject weapon3;

    [Header(" Ice Spikes attack")] 
    public float w4AttackRateSec;
    public float w4AttackDMG;
    public float w4AttkDuration;

    [Header("Health Settings")]

    public Dictionary<string, float> WeaponDMG = new Dictionary<string, float>();
    public Dictionary<string, float> WeaponRateSec = new Dictionary<string, float>();
    public Dictionary<string, float> WeaponAttDuration = new Dictionary<string, float>();
    void Awake()
    {
        //DMG
        WeaponDMG["AreaAttack"] = w1AttackDMG;
        WeaponDMG["BookAttack"] = w2AttackDMG;
        WeaponDMG["SpearAttack"] = w3AttackDMG;
        WeaponDMG["SpikesAttack"] = w4AttackDMG;


        // Rate in seconds
        WeaponRateSec["AreaAttack"] = w1AttackRateSec;
        WeaponRateSec["BookAttack"] = w2AttackRateSec;
        WeaponRateSec["SpearAttack"] = w3AttackRateSec;
        WeaponRateSec["SpikesAttack"] = w4AttackRateSec;

        // Attack duration
        WeaponAttDuration["AreaAttack"] = w1AttkDuration;
        WeaponAttDuration["BookAttack"] = w2AttkDuration;
        WeaponAttDuration["SpearAttack"] = w3AttkDuration;
        WeaponAttDuration["SpikesAttack"] = w4AttkDuration;
    }
    public void UpdateWeaponStats(string weaponType, float newDamage, 
        float newRateSec, float newDuration) // function used to update weapon stats 
    {
        if (WeaponDMG.ContainsKey(weaponType))
        {
            WeaponDMG[weaponType] = newDamage;
        }
        if (WeaponRateSec.ContainsKey(weaponType))
        {
            WeaponRateSec[weaponType] = newRateSec;
        }
        if (WeaponAttDuration.ContainsKey(weaponType))
        {
            WeaponAttDuration[weaponType] = newDuration;
        }
    }

}
