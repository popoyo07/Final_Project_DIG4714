using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;


public class UIBars : MonoBehaviour
{
    public Image XPImage;
    public Image UltImage;
    public Image HealthImage;

    public float currentXP;
    public float currentUlt;
    private float currentHealth;

    public float MaxXP;
    public float MaxUlt;
    public float MaxHealth;

    public int PlayerLevel = 0;

    private PlayerController playerController;

    public bool leveledUp = false;


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
        playerController = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerController.health;
    }



    public void LevelUP ()
    {
        currentXP -= MaxXP;
        MaxXP = MaxXP * 2f;
        leveledUp = true;
        Debug.Log("MaxXP:" + MaxXP);
    }


    public void GainXPbar(float xp)
    {
        currentXP += xp;
        if (currentXP >= MaxXP) // Check if XP exceeds required amount
        {
            PlayerLevel++;
            LevelUP();
        }

        XPImage.fillAmount = currentXP / MaxXP; // Update XP bar after leveling
        Debug.Log("Player's level: " + PlayerLevel + "Fill Amount: " + XPImage.fillAmount);
    }



    public void GainUltBar(float xp)
    {
        currentUlt += xp/2;
        UltImage.fillAmount = currentUlt/MaxUlt;

    }

    public void LoseHealthBar(float currentHealth) 
    {
        HealthImage.fillAmount = currentHealth/MaxHealth;
    }

}
