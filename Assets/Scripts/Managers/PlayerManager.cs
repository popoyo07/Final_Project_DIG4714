using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;
    //private bool[] playerChoice;
    [SerializeField]GameObject[] players;
    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
       
    }
    void Start()
    {
        if (gameManager != null)
        {
            players[0].SetActive(false);
            players[1].SetActive(false);
            players[2].SetActive(false);
            // playerChoice[1] = false;
            // playerChoice[2] = false;
            // playerChoice[gameManager.pChoice] = true;

            players[gameManager.pChoice].SetActive(true);
        }
    }

 

}
