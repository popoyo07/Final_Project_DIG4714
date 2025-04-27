using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    private UIBars UIBars;
    public GameObject LoseScene;
    private GameObject player;
    private JsonSaveExample save;
    public TextMeshProUGUI coinCollected;
    private MusicManager musicManager;

    bool added;

    public GameObject[] allPlayers;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        save = GameObject.FindWithTag("GameManager").GetComponent<JsonSaveExample>();
        foreach (GameObject p in allPlayers)
        {
            if (p.CompareTag("Player"))
            {
                player = p;
                break;
            }
        }
        musicManager = FindObjectOfType<MusicManager>();
        UIBars = player.GetComponent<UIBars>();
        playerController = player.GetComponent<PlayerController>();
        LoseScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (UIBars.playerAlive == false)
        {
            Debug.LogWarning(UIBars.playerAlive + "playerController.health current health:" + playerController.health + "UI bar currentHealth:" + UIBars.currentHealth);
            musicManager.PauseMusic();

            if (!added)
            {
                added = true;
                save.addCoinsEnd();
                save.SaveData();
            }

            LoseScene.SetActive(true);
            coinCollected.text = "Coin Collected: " + save.lastCoins.ToString();
            Time.timeScale = 0f;

        }
    }
}
