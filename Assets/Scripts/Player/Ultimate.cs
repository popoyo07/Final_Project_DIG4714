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
            if (UIBars.currentUlt == 100 && Input.GetKeyUp(KeyCode.Space)) 
            {
                playerController.health += 50f;
                if (playerController.health >= 100f) 
                {
                    playerController.health = 100f;
                }
            }
        }
    }
}
