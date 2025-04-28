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

    public int PlayerLevel = 0;
    public float xpMultiplier = 1f;
    public float ultMultiplier = 1f;

    private PlayerController playerController;

    public bool leveledUp = false;
    public bool playerAlive;

    private void Awake()
    {
        playerAlive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
/*        if (MaxXP <= 0f)
        {
            MaxXP = 100f;
        }

        if (MaxUlt <= 0f)
        {
            MaxUlt = 100f;
        }

        if (MaxHealth <= 0f)
        {
            MaxHealth = 100f;
        }*/

        currentXP = 0f;
        XPImage.fillAmount = currentXP / MaxXP;
        currentUlt = 0f;
        UltImage.fillAmount = currentUlt / MaxUlt;
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogWarning("NO PLAYER CONTROLLER");
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerController.health;

       if (currentHealth <= 0f) 
       {
            playerAlive = false;
       }
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
        currentXP += xp * xpMultiplier;
        if (currentXP >= MaxXP) // Check if XP exceeds required amount
        {
            PlayerLevel++;
            LevelUP();
        }

        XPImage.fillAmount = currentXP / MaxXP; // Update XP bar after leveling
        Debug.Log("Player's level: " + PlayerLevel + "Fill Amount: " + XPImage.fillAmount);
    }



    public void GainUltBar(float ult)
    {
        currentUlt += ult * ultMultiplier;
        if (currentUlt >= MaxUlt)
        {
            currentUlt = MaxUlt;
        }

        UltImage.fillAmount = currentUlt/MaxUlt;

        if (currentUlt == MaxUlt)
        {
            UltImage.color = Color.yellow;
        }

    }

    public void LoseHealthBar(float currentHealth) 
    {
        HealthImage.fillAmount = currentHealth/MaxHealth;
    }


}
