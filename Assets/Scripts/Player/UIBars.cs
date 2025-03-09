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

        if (MaxUlt <= 0f)
        {
            MaxUlt = 100f;
        }

        if (MaxHealth <= 0f)
        {
            MaxHealth = 10f;
        }

        currentXP = 0f;
        XPImage.fillAmount = currentXP / MaxXP;
        currentUlt = 0f;
        UltImage.fillAmount = currentUlt / MaxUlt;
        currentHealth = MaxHealth;


    }

    // Update is called once per frame
    void Update()
    {
        


    }




    public void GainXPbar(float xp)
    {
        currentXP += xp;
        XPImage.fillAmount = currentXP / MaxXP;
    }


    public void GainUltBar(float xp)
    {
        currentUlt += xp/2;
        UltImage.fillAmount = currentUlt/MaxUlt;

    }

    public void LoseHealth(float attackDamage) 
    {
        currentHealth -= attackDamage;
        HealthImage.fillAmount = currentHealth/MaxHealth;
    }

}
