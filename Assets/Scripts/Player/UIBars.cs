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
        MaxXP = MaxXP * 1.5f;
        Debug.Log("MaxXP:" + MaxXP);
    }


    public void GainXPbar(float xp)
    {
        currentXP += xp;
        XPImage.fillAmount = currentXP / MaxXP;
        if (XPImage.fillAmount >= 1f)
        {
            PlayerLevel++;
            LevelUP();
            Debug.Log("Player's level: "+ PlayerLevel);
        }
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
