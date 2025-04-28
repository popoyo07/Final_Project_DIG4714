using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]GameObject[] players;
    CinemachineVirtualCamera virtualCamera;


    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
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
            virtualCamera.Follow = players[gameManager.pChoice].transform; // updates the virtual camera to follow the active player
            Debug.LogWarning(players[gameManager.pChoice]);
        }
    }

 

}
