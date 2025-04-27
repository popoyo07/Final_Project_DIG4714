using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]GameObject[] players;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        //  depending on which integer player character is chsoen,
        //  it will determine which player game object will be enabled 
        if (gameManager != null)
        {
            players[0].SetActive(false);
            players[1].SetActive(false);
            players[2].SetActive(false);

            players[gameManager.pChoice].SetActive(true);
            Debug.LogWarning(players[gameManager.pChoice]);
        }
    }

 

}
