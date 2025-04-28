using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Xml.Serialization;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject[] players;
    CinemachineVirtualCamera virtualCamera;
    [HideInInspector] public int active;

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

            active = gameManager.pChoice;
            players[active].SetActive(true);
            virtualCamera.Follow = players[active].transform; // updates the virtual camera to follow the active player
            Debug.LogWarning(players[active]);
        }
    }

 

}
