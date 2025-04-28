using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    // activates ultimate if player 
    UIBars UIBars;
    PlayerController playerController;
    void Start()
    {
        UIBars = gameObject.GetComponent<UIBars>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

  
    void Update()
    {
        if (UIBars != null) 
        {
            if (UIBars.currentUlt >= 70 && Input.GetKeyUp(KeyCode.Space)) 
            {
                UseUltimate();
            }
        }
    }

    void UseUltimate()
    {
        playerController.health += 50f;
            if (playerController.health >= 100f)
            {
                playerController.health = 100f;
            }
        UIBars.HealthImage.fillAmount = playerController.health / 100;
        UIBars.currentUlt = 0f;
        UIBars.UltImage.fillAmount = 0f;
        UIBars.UltImage.color = new Color(0.3137254901960784f, 1f, 0.8980392156862745f);
        Debug.Log("Ultimate activated!");
    }
}
