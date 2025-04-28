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
    GameManager gameManager;

    public AudioClip SFXSanta;
    public AudioClip SFXMrsClaus;
    public AudioClip SFXRudolph;

    private AudioSource audioSource;

    bool playedAudio;
    bool added;

    public GameObject[] allPlayers;
    private PlayerController playerController;
    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }
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

        player = allPlayers[gameManager.pChoice];
        musicManager = FindObjectOfType<MusicManager>();
        UIBars = player.GetComponent<UIBars>();
        playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        LoseScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIBars.playerAlive == false)
        {
            Debug.LogWarning(UIBars.playerAlive + "playerController.health current health:" + playerController.health + "UI bar currentHealth:" + UIBars.currentHealth);
            musicManager.PauseMusic();
            if (!playedAudio)
            {
                playedAudio = true;

                if (gameManager.pChoice == 0)
                {
                    audioSource.clip = SFXSanta;
                }
                else if (gameManager.pChoice == 1)
                {
                    audioSource.clip = SFXMrsClaus;
                }
                else if (gameManager.pChoice == 2)
                {
                    audioSource.clip = SFXRudolph;
                }

                audioSource.Play();
            }

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
