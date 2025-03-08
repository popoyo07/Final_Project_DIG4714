using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBars : MonoBehaviour
{
    public Image XPImage;
    public Image UltImage;
    public Image HealthImage;

    public float currentXP;
    public float currentUlt;
    public float currentHealth;

    public float MaxXP;
    public float MaxUlt;
    public float MaxHealth;




    // Start is called before the first frame update
    void Start()
    {
        if (MaxXP <= 0f)
        {
            MaxXP = 100f;
        }

        if (MaxUlt < 0f)
        {
            MaxUlt = 100f;
        }

        if (MaxHealth <= 0f)
        {
            MaxHealth = 10f;
        }

        currentXP = 0f;
        currentUlt = 0f;
        currentHealth = MaxHealth;


    }

    // Update is called once per frame
    void Update()
    {
        


    }




    public void LevelUP(float xp) 
    {
        currentXP += xp;
    }

    public void UltReady(float xp)
    {
        currentUlt += xp/5;

    }

    public void LoseHealth(float attackDamage) 
    {
        currentHealth -= attackDamage;

    }

}
