using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using static Cinemachine.DocumentationSortingAttribute;

public class Weapons : MonoBehaviour
{
    [Header(" Weapon Enabler")] // reference to enable or disable weapons 
    public bool spikesAttack;
    public bool baseAttack;
    public bool bookAttack ;
    public bool spearAttack;

    [Header(" Weapon References")]
    public GameObject[] weapons;

    [Header(" Unlock weapons References")]
    public GameObject Canvas;
    private LevelUpCanvas levelUpCanvas;

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

    private void Update()
    {
        // DMG
        checkToUpdateVar(WeaponDMG, "AreaAttack", w1AttackDMG);
        checkToUpdateVar(WeaponDMG, "BookAttack", w2AttackDMG);
        checkToUpdateVar(WeaponDMG, "SpearAttack", w3AttackDMG);
        checkToUpdateVar(WeaponDMG, "SpikesAttack", w4AttackDMG);

        // Rate in seconds
        checkToUpdateVar(WeaponRateSec, "AreaAttack", w1AttackRateSec);
        checkToUpdateVar(WeaponRateSec, "BookAttack", w2AttackRateSec);
        checkToUpdateVar(WeaponRateSec, "SpearAttack", w3AttackRateSec);
        checkToUpdateVar(WeaponRateSec, "SpikesAttack", w4AttackRateSec);

        // Attack duration
        checkToUpdateVar(WeaponAttDuration, "AreaAttack", w1AttkDuration);
        checkToUpdateVar(WeaponAttDuration, "BookAttack", w2AttkDuration);
        checkToUpdateVar(WeaponAttDuration, "SpearAttack", w3AttkDuration);
        checkToUpdateVar(WeaponAttDuration, "SpikesAttack", w4AttkDuration);


    }
    // check if the value changed and updates the variable 
    void checkToUpdateVar(Dictionary<string, float> dictornary, string key, float updatedValue) 
    {
        if (dictornary[key] != updatedValue) 
        {
            dictornary[key] = updatedValue;
            Debug.Log("Now the value is: " +  dictornary[key]);
        }
    }

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

        // Enable Attack
        baseAttack = true;
        spikesAttack = false;
        spearAttack = false;
        bookAttack = false;

        // Reference Canvas
        levelUpCanvas = Canvas.GetComponent<LevelUpCanvas>();
        if (levelUpCanvas == null)
        {
            Debug.Log("No canvas");
        }
    }


    public void UnlockBook()
    {
        bookAttack = true;
        weapons[1].SetActive(true);
    }

    public void UnlockSpike()
    {
        spikesAttack = true;
        weapons[2].SetActive(true);
    }

    public void UnlockSpear()
    {
        spearAttack = true;
        weapons[3].SetActive(true);
    }

    public void UpdateWeaponDMG(string weaponType, float newDamage) // adds more damage to current dmg  
    {
        if (WeaponDMG.ContainsKey(weaponType))
        {
            WeaponDMG[weaponType] += newDamage;
        }


    }

    public void UpdateWeaponRate(string weaponType, float newRate) // - time means faster rate per second   
    {
        if (WeaponRateSec.ContainsKey(weaponType))
        {
            WeaponRateSec[weaponType] -= newRate;
        }
    }

    public void UpdateWeaponDuration(string weaponType, float newDuration) // increase duration 
    {
        if (WeaponAttDuration.ContainsKey(weaponType))
        {
            WeaponAttDuration[weaponType] += newDuration;
        }


    }
}
